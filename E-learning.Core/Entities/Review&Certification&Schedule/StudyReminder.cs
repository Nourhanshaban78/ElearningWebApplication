using E_learning.Core.Entities.Identity;

namespace E_learning.Core.Entities.Review_Certification_Schedule
{
    public class StudyReminder
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string Title { get; set; }

        public TimeSpan ReminderTime { get; set; }

        public bool IsDaily { get; set; } = true;

        public DateTime? SpecificDate { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public ApplicationUser User { get; set; }
    }
}
