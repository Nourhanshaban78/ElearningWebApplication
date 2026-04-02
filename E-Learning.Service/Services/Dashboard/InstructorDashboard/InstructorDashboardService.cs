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

        private Guid GetInstructorId()
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
            var instructorId = GetInstructorId();
            var now = DateTime.UtcNow;

            // ─── 1. كورسات الانستراكتور ───
            var courses = await _unit.Courses
                .QueryNoTracking()
                .Where(c => c.InstructorId == instructorId && !c.IsDeleted)
                .Select(c => new { c.Id, c.Title })
                .ToListAsync(ct);

            if (!courses.Any())
                return _response.Success(new InstructorDashboardDto());

            var courseIds = courses.Select(c => c.Id).ToList();

            // ─── 2. Enrollments ───
            var enrollments = await _unit.Enrollments
                .QueryNoTracking()
                .Where(e => courseIds.Contains(e.CourseId) && !e.IsDeleted)
                .Select(e => new
                {
                    e.CourseId,
                    e.StudentId,
                    e.Status,
                    e.ProgressPercentage
                })
                .ToListAsync(ct);

            // ─── 3. Exams ───
            var activeExams = await _unit.Exams
                .QueryNoTracking()
                .CountAsync(e => courseIds.Contains(e.CourseId)
                              && e.IsActive
                              && e.ScheduledAt <= now
                              && (e.EndDateTime == null || e.EndDateTime >= now), ct);

            // ─── 4. Quizzes ───
            var activeQuizzes = await _unit.Quizzes
                .QueryNoTracking()
                .CountAsync(q => courseIds.Contains(q.CourseId) && q.IsActive, ct);

            // ─── 5. Weekly Engagement ───
            var daysFromMon = ((int)now.DayOfWeek + 6) % 7;
            var weekStart = now.Date.AddDays(-daysFromMon);
            var weekEnd = weekStart.AddDays(7);

            var lessonAccess = await _unit.LessonProgresses
                .QueryNoTracking()
                .Where(lp => courseIds.Contains(lp.Enrollment.CourseId)
                          && !lp.Enrollment.IsDeleted
                          && lp.LastAccessedAt >= weekStart
                          && lp.LastAccessedAt < weekEnd)
                .Select(lp => new { lp.EnrollmentId, lp.LastAccessedAt })
                .ToListAsync(ct);

            // ─── 6. Live Sessions ───
            var liveSessions = await _unit.LiveSessions
                .QueryNoTracking()
                .Include(ls => ls.Course)
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

            // ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
            // كل الحسابات في الـ Memory
            // ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

            var dayNames = new[] { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" };
            var totalStudents = enrollments.Select(e => e.StudentId).Distinct().Count();

            var weekly = Enumerable.Range(0, 7).Select(i =>
            {
                var date = weekStart.AddDays(i);
                var unique = lessonAccess
                    .Where(lp => lp.LastAccessedAt.Date == date)
                    .Select(lp => lp.EnrollmentId)
                    .Distinct()
                    .Count();
                return new WeeklyEngagementPointDto
                {
                    Day = dayNames[i],
                    EngagementRate = totalStudents > 0
                        ? Math.Round((double)unique / totalStudents * 100, 1)
                        : 0
                };
            }).ToList();

            var progresses = enrollments
                .Where(e => e.ProgressPercentage > 0)
                .Select(e => e.ProgressPercentage)
                .ToList();

            var performance = new StudentPerformanceDistributionDto
            {
                Excellent = progresses.Count(p => p >= 90),
                VeryGood = progresses.Count(p => p >= 75 && p < 90),
                Good = progresses.Count(p => p >= 60 && p < 75),
                NeedsImprovement = progresses.Count(p => p < 60),
                TotalEvaluated = progresses.Count
            };

            var completionRates = courses.Select(c =>
            {
                var courseEnrollments = enrollments.Where(e => e.CourseId == c.Id).ToList();
                var total = courseEnrollments.Count;
                var completed = courseEnrollments.Count(e =>
                    e.Status == EnrollmentStatus.Completed || e.ProgressPercentage >= 100);
                return new CourseCompletionRateDto
                {
                    CourseId = c.Id,
                    CourseTitle = c.Title,
                    TotalStudents = total,
                    CompletionRate = total > 0
                        ? Math.Round((decimal)completed / total * 100, 1)
                        : 0
                };
            }).OrderByDescending(c => c.CompletionRate).ToList();

            var avgCompletion = completionRates.Any()
                ? Math.Round(completionRates.Average(c => c.CompletionRate), 1)
                : 0;

            return _response.Success(new InstructorDashboardDto
            {
                Stats = new DashboardOverviewStatsDto
                {
                    TotalCourses = courses.Count,
                    TotalStudents = totalStudents,
                    ActiveExams = activeExams,
                    ActiveQuizzes = activeQuizzes
                },
                WeeklyEngagement = weekly,
                PerformanceDistribution = performance,
                CourseCompletionRates = completionRates,
                AverageCompletionRate = avgCompletion,
                UpcomingLiveSessions = liveSessions
            });
        }
            
    }
}
