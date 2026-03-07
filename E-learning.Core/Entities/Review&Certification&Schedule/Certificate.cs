using E_learning.Core.Entities.Courses___content;
using E_learning.Core.Entities.Identity;
using E_learning.Core.Entities.Profiles;

namespace E_learning.Core.Entities.Review_Certification_Schedule
{
    public class Certificate
    {
        public Guid Id { get; set; }

        public Guid StudentId { get; set; }
        public Guid CourseId { get; set; }

        public string CertificateCode { get; set; }

        public DateTime IssuedAt { get; set; } = DateTime.UtcNow;

        public string? FileUrl { get; set; }

        public Student Student { get; set; }
        public Courses Course { get; set; }
    }
}
