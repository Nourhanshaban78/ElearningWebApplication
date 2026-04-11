using E_Learning.Core.Entities.Enrollment;

namespace E_Learning.Core.Interfaces.Repositories
{
    public interface IStudentDashboardRepository
    {
        Task<int> GetEnrolledCoursesCountAsync(Guid studentId);
        Task<int> GetCompletedLessonsCountAsync(Guid studentId);
        Task<int> GetPendingTasksCountAsync(Guid studentId);
        Task<int> GetUpcomingExamsCountAsync(Guid studentId);
        
        // لجلب بيانات الدرس الحالي والنسبة (مثل الصورة)
          Task<List<StudentCourseProgressDto>> GetAllCoursesProgressAsync(Guid studentId);

    }

    public class StudentCourseProgressDto
        { public string CourseName { get; set; } = string.Empty;
        public string CurrentLessonName { get; set; } = string.Empty;
        public decimal ProgressPercentage { get; set; }
        }
}