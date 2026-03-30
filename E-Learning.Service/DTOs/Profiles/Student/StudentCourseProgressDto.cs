namespace E_Learning.Core.DTOs
{
    public class StudentCourseProgressDto
    {
        public string CourseName { get; set; } = string.Empty;
        public string CurrentLessonName { get; set; } = string.Empty;
        public decimal ProgressPercentage { get; set; }
    }
}