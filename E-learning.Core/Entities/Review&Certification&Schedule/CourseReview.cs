using E_learning.Core.Entities.Courses___content;
using E_learning.Core.Entities.Identity;

namespace E_learning.Core.Entities.Review_Certification_Schedule
{
    public class CourseReview
    {
        public int Id { get; set; }

        public int CourseId { get; set; }
        public string StudentId { get; set; }

        public int Rating { get; set; }

        public string? Comment { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string? InstructorReply { get; set; }
        public DateTime? InstructorRepliedAt { get; set; }

        // Navigation
        public Courses Course { get; set; }
        public ApplicationUser Student { get; set; }
    }
}
