using E_Learning.Core.Entities.Enrollment;
using E_Learning.Core.Enums;
using E_Learning.Core.Interfaces.Repositories;
using E_Learning.Repository.Data;
using Microsoft.EntityFrameworkCore;

namespace E_Learning.Infrastructure.Repositories
{
    public class StudentDashboardRepository : IStudentDashboardRepository
    {
        private readonly ELearningDbContext _context;

        public StudentDashboardRepository(ELearningDbContext context)
        {
            _context = context;
        }

        public async Task<int> GetEnrolledCoursesCountAsync(Guid studentId)
        {
            return await _context.Enrollments
                .CountAsync(e => e.StudentId == studentId && !e.IsDeleted);
        }

        public async Task<int> GetCompletedLessonsCountAsync(Guid studentId)
        {
            return await _context.LessonProgresses
                .CountAsync(lp => lp.Enrollment.StudentId == studentId
                                  && lp.Status == LessonProgressStatus.Completed);
        }

        public async Task<int> GetPendingTasksCountAsync(Guid studentId)
        {
            return await _context.AssignmentSubmissions
                .CountAsync(s => s.StudentId == studentId
                                 && s.Status == AssignmentStatus.Pending );
        }

        public async Task<int> GetUpcomingExamsCountAsync(Guid studentId)
        {
            return await _context.Exams
                .CountAsync(ex => ex.IsActive
                                  && ex.ScheduledAt > DateTime.UtcNow
                                  && _context.Enrollments.Any(e => e.StudentId == studentId && e.CourseId == ex.CourseId));
        }

        // الحل النهائي باستخدام الـ Join اليدوي
        public async Task<List<StudentCourseProgressDto>> GetAllCoursesProgressAsync(Guid studentId)
        {
            var query = from enrollment in _context.Enrollments
                        join lessonProgress in _context.LessonProgresses
                        on enrollment.Id equals lessonProgress.EnrollmentId into lpGroup
                        where enrollment.StudentId == studentId && !enrollment.IsDeleted
                        select new StudentCourseProgressDto
                        {
                            CourseName = enrollment.Course.Title,
                            ProgressPercentage = enrollment.ProgressPercentage,
                            // البحث في المجموعة المرتبطة عن آخر درس لم يكتمل
                            CurrentLessonName = lpGroup
                                .Where(lp => lp.Status != LessonProgressStatus.Completed)
                                .OrderByDescending(lp => lp.LastAccessedAt)
                                .Select(lp => lp.Lesson.Title)
                                .FirstOrDefault() ?? "No lessons started yet"
                        };

            // ضروري جداً استخدام ToListAsync() هنا
            return await query.ToListAsync();
        }
    }
}