using E_learning.Core.Entities.Courses___content;
using E_learning.Core.Entities.Identity;

namespace E_learning.Core.Entities.Review_Certification_Schedule
{
    public class Certificate
    {
        public int Id { get; set; }

        public string StudentId { get; set; }
        public int CourseId { get; set; }

        public string CertificateCode { get; set; }

        public DateTime IssuedAt { get; set; } = DateTime.UtcNow;

        public string? FileUrl { get; set; }

        // Navigation
        public ApplicationUser Student { get; set; }
        public Courses Course { get; set; }
    }
}
