
using E_Learning.Core.Interfaces.Repositories;

namespace E_Learning.Service.DTOs.Profiles.Student
{
    public class StudentDashboardDto
    {
        public int EnrolledCoursesCount { get; set; }
        public int CompletedLessonsCount { get; set; }
        public int PendingTasksCount { get; set; }
        public int UpcomingExamsCount { get; set; }
public List<StudentCourseProgressDto> AllCourses { get; set; } = new();    }

 
}