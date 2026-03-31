using E_Learning.Core.Base;
using E_Learning.Core.Enums;
using E_Learning.Core.Repository;
using E_Learning.Service.DTOs.Dashboard.Instructor_Dashboard;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Service.Services.Dashboard.InstructorDashboard
{
    public class InstructorDashboardService : IInstructorDashboardService
    {
        private readonly IUnitOfWork _unit;
        private readonly ResponseHandler _response;
        private readonly IHttpContextAccessor _contextAccessor;

        public InstructorDashboardService(IUnitOfWork unit, ResponseHandler response, IHttpContextAccessor httpContext)
        {
            _unit = unit;
            _response = response;
            _contextAccessor = httpContext;
        }

        private Guid GetInstructorID()
        {
              var claim = _contextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(claim))
            {
                throw new UnauthorizedAccessException("Instructor not authenticated.");
            }
            return Guid.Parse(claim);
        }

        public async Task<Response<InstructorDashboardDto>> GetDashboardAsync(CancellationToken ct = default)
        {
            var InstructorId = GetInstructorID();

            var courseIds = await _unit.Courses.QueryNoTracking().Where(c=>c.InstructorId == InstructorId && !c.IsDeleted)
                .Select(c=>c.Id).ToListAsync(ct);
           
            if(!courseIds.Any())  return _response.Success(new InstructorDashboardDto());

            var statsTask = GetOverviewStatsAsync(courseIds, ct);
            var weeklyTask = GetWeeklyEngagementAsync(courseIds, ct);
            var performanceTask = GetPerformanceDistributionAsync(courseIds, ct);
            var completionTask = GetCourseCompletionRatesAsync(courseIds, ct);
            var liveSessionsTask = GetUpcomingLiveSessionsAsync(instructorId, ct);

            await Task.WhenAll(statsTask, weeklyTask, performanceTask, completionTask, liveSessionsTask);

            var completionRates = await completionTask;
            var avgCompletion = completionRates.Any()
                ? Math.Round(completionRates.Average(c => c.CompletionRate), 1)
                : 0;

            var dashboard = new InstructorDashboardDto
            {
                Stats = await statsTask,
                PerformanceDistribution = await performanceTask,
                CourseCompletionRates = await completionRates,
                AverageCompletionRate = await avgCompletion,
                UpcomingLiveSessions = await liveSessionsTask,
            };
            return _response.Success(dashboard);
        }
        // ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
        // 1. Overview Stats
        // ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
        private async Task<DashboardOverviewStatsDto> GetOverviewStatsAsync(List<int>courseIds,CancellationToken ct = default)
        {
            var now = DateTime.UtcNow;

            var totalStudents = await _unit.Enrollments.QueryNoTracking()
                .Where(e=>courseIds.Contains(e.CourseId) && !e.IsDeleted)
                .Select(e=> e.StudentId).Distinct().CountAsync();

            var activeExams = await _unit.Exams.CountAsync(
                e => courseIds.Contains(e.CourseId) && e.IsActive && e.ScheduledAt <= now &&
                e.EndDateTime == null || e.EndDateTime >= now, ct);

            var activeQuizzes = await _unit.Quizzes.CountAsync(q=> courseIds.Contains(q.CourseId) && q.IsActive,ct);

            return new DashboardOverviewStatsDto
            {
                TotalCourses = courseIds.Count(),
                TotalStudents = totalStudents,
                ActiveExams = activeExams,
                ActiveQuizzes = activeQuizzes
            };
        }

        // ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
        // 2. Weekly Student Engagement
        // ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
        private async Task<List< WeeklyEngagementPointDto>> GetWeeklyEngagementAsync(List<int> courseIds,
        CancellationToken ct)
        {
            var today = DateTime.UtcNow.Date;
            var daysFromMon = ((int)today.DayOfWeek + 6) % 7;
            var weekStart = today.AddDays(-daysFromMon);
            var weekEnd = weekStart.AddDays(7);

            var dayNames = new[] { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" };

            var enrollmentIds = await _unit.Enrollments.QueryNoTracking()
                .Where(e=>courseIds.Contains(e.CourseId) && !e.IsDeleted).Select(e => e.Id).ToListAsync(ct);

            if (!enrollmentIds.Any())
                return dayNames.Select(d => new WeeklyEngagementPointDto { Day = d }).ToList();

            var totalStudents = await _unit.Enrollments.QueryNoTracking()
               .Where(e => courseIds.Contains(e.CourseId) && !e.IsDeleted)
               .Select(e => e.StudentId).Distinct().CountAsync(ct);

            // Group LessonProgress by day
            var dailyData = await _unit.LessonProgresses.QueryNoTracking().Where(lp => enrollmentIds.Contains(lp.EnrollmentId)
                      && lp.LastAccessedAt >= weekStart
                      && lp.LastAccessedAt < weekEnd)
            .GroupBy(lp => lp.LastAccessedAt.Date).Select(g => new
            {
                Date = g.Key,
                UniqueEnrollments = g.Select(x => x.EnrollmentId).Distinct().Count()
            }).ToListAsync(ct);

            return Enumerable.Range(0, 7).Select(i =>
            {
                var date = weekStart.AddDays(i);
                var access = dailyData.FirstOrDefault(d => d.Date == date);
                var rate = totalStudents > 0 && access != null
                    ? Math.Round((double)access.UniqueEnrollments / totalStudents * 100, 1)
                    : 0;

                return new WeeklyEngagementPointDto
                {
                    Day = dayNames[i],
                    EngagementRate = rate
                };
            }).ToList();

        }

        // ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
        // 3. Student Performance Distribution (Pie Chart)
        // ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
        private async Task<StudentPerformanceDistributionDto> GetPerformanceDistributionAsync(
        List<int> courseIds,CancellationToken ct)
        {
            var progresses = await _unit.Enrollments.QueryNoTracking()
                .Where(e => courseIds.Contains(e.CourseId)&& !e.IsDeleted
                         && e.ProgressPercentage > 0)
                .Select(e => e.ProgressPercentage).ToListAsync(ct);
            return new StudentPerformanceDistributionDto
            {
                Excellent = progresses.Count(p => p >= 90),
                VeryGood = progresses.Count(p => p >= 75 && p < 90),
                Good = progresses.Count(p => p >= 60 && p < 75),
                NeedsImprovement = progresses.Count(p => p < 60),
                TotalEvaluated = progresses.Count
            };
        }

        // ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
        // 4. Course Completion Rate (Bar Chart)
        // ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
        private async Task<List<CourseCompletionRateDto>> GetCourseCompletionRatesAsync(
        List<int> courseIds,
        CancellationToken ct)
            {
                var courses = await _unit.Courses
                    .QueryNoTracking()
                    .Where(c => courseIds.Contains(c.Id))
                    .Select(c => new { c.Id, c.Title })
                    .ToListAsync(ct);

                var stats = await _unit.Enrollments
                    .QueryNoTracking()
                    .Where(e => courseIds.Contains(e.CourseId) && !e.IsDeleted)
                    .GroupBy(e => e.CourseId).Select(g => new
                    {
                        CourseId = g.Key,
                        TotalStudents = g.Count(),
                        Completed = g.Count(e => e.Status == EnrollmentStatus.Completed
                                                  || e.ProgressPercentage >= 100)
                    }).ToListAsync(ct);

                return courses.Select(c =>
                {
                    var s = stats.FirstOrDefault(x => x.CourseId == c.Id);
                    var rate = s?.TotalStudents > 0
                        ? Math.Round((decimal)s.Completed / s.TotalStudents * 100, 1): 0m;

                    return new CourseCompletionRateDto
                    {
                        CourseId = c.Id,
                        CourseTitle = c.Title,
                        TotalStudents = s?.TotalStudents ?? 0,
                        CompletionRate = rate
                    };
                })
                .OrderByDescending(c => c.CompletionRate).ToList();
            }

        // ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
        // 5. Upcoming Live Sessions
        // ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
        private async Task<List<UpcomingLiveSessionDto>> GetUpcomingLiveSessionsAsync(
            Guid instructorId,
            CancellationToken ct)
        {
            var now = DateTime.UtcNow;

            return await _unit.LiveSessions.QueryNoTracking()
                .Where(ls => ls.InstructorId == instructorId
                          && ls.ScheduledAt > now
                          && ls.Status == LiveSessionStatus.Scheduled)
                .OrderBy(ls => ls.ScheduledAt)
                .Take(5)
                .Select(ls => new UpcomingLiveSessionDto
                {
                    SessionId = ls.Id,
                    Title = ls.Title,
                    CourseName = ls.Course.Title,
                    ScheduledAt = ls.ScheduledAt,
                    DurationMinutes = ls.DurationMinutes,
                    Status = ls.Status.ToString()
                })
                .ToListAsync(ct);
        }

    }
}
