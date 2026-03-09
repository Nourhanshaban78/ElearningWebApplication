using E_learning.Core.Entities.Courses___content;
using E_learning.Core.Entities.Identity;
using E_learning.Core.Entities.Profiles;

namespace E_learning.Core.Entities.Review_Certification_Schedule
{
    public class ScheduleEvent
    {
        public Guid Id { get; set; }

        public Guid InstructorId { get; set; }

        public Guid? CourseId { get; set; }

        public string Title { get; set; }

        public string Type { get; set; }

        public string Priority { get; set; } = "Medium";

        public DateTime StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public Instructor Instructor { get; set; } = null!;
        public Course? Course { get; set; } = null!;
    }
}
