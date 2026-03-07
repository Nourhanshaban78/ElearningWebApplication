using Microsoft.AspNetCore.Identity;
using E_learning.Core.Entities.Billing___Payments;
using E_learning.Core.Entities.Courses___content;
using E_learning.Core.Enums;
using E_learning.Core.Entities.Notifactions;
using E_learning.Core.Entities.Profiles;
using E_learning.Core.Entities.Enrollment___Progress;


namespace E_learning.Core.Entities.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FullName { get; set; } = string.Empty;
        public string? Bio { get; set; }
        public string? ProfileImage { get; set; }
        public string? Location { get; set; }
        public DateOnly? DateOfBirth { get; set; }

        public Status IsActive { get; set; }
        public DateTime MemberSince { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Notifications
        public ICollection<Notifications> Notifications { get; set; } = new List<Notifications>();
        public NotificationSettings? NotificationSettings { get; set; }

        // Relations
        public ICollection<Courses> Courses { get; set; } = new List<Courses>();
        public ICollection<Courses> ApprovedCourses { get; set; } = new List<Courses>();
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        public ICollection<PaymentMethods> PaymentMethods { get; set; } = new List<PaymentMethods>();
        public ICollection<PayoutRequests> PayoutRequests { get; set; } = new List<PayoutRequests>();
        public ICollection<UserSession> UserSessions { get; set; } = new List<UserSession>();
        public ICollection<OtpCodes> OtpCodes { get; set; } = new List<OtpCodes>();

        // Profiles
        public Student? Student { get; set; }
        public Instructor? Instructor { get; set; }
        public Admin? Admin { get; set; }
    }
}
