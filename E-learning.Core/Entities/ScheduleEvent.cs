using E_learning.Core.Entities.Identity;

namespace E_learning.Core.Entities
{
    public class ScheduleEvent
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public int? CourseId { get; set; }

        public string Title { get; set; }

        public string Type { get; set; }

        public string Priority { get; set; } = "Medium";

        public DateTime StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public ApplicationUser User { get; set; }
        //public Course? Course { get; set; }
    }
}
