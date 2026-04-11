using E_Learning.Core.Interfaces.Repositories;
using E_Learning.Service.Services.UserDashboard;
using E_Learning.Service.DTOs.Profiles.Student;
using E_Learning.Core.DTOs; // تأكدي من إضافة هذا الـ using للوصول للـ DTO الموحد

namespace E_Learning.Application.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IStudentDashboardRepository _repo;

        public DashboardService(IStudentDashboardRepository repo)
        {
            _repo = repo;
        }

     public async Task<StudentDashboardDto> GetStudentDashboardDataAsync(Guid studentId)
{
    var enrolledCount = await _repo.GetEnrolledCoursesCountAsync(studentId);
    var completedCount = await _repo.GetCompletedLessonsCountAsync(studentId);
    var pendingTasks = await _repo.GetPendingTasksCountAsync(studentId);
    var upcomingExams = await _repo.GetUpcomingExamsCountAsync(studentId);
    
    // الآن allCoursesList ستكون من نوع List<E_Learning.Core.DTOs.StudentCourseProgressDto>
    // لأن الـ Interface أصبح يقرأ من هناك
    var allCoursesList = await _repo.GetAllCoursesProgressAsync(studentId);

    return new StudentDashboardDto
    {
        EnrolledCoursesCount = enrolledCount,
        CompletedLessonsCount = completedCount,
        PendingTasksCount = pendingTasks,
        UpcomingExamsCount = upcomingExams,
        AllCourses = allCoursesList // لن يظهر خطأ هنا بعد الآن
    };
}
    }
}