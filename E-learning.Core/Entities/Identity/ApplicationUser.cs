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
        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
        public NotificationSetting? NotificationSettings { get; set; }

        // Relations
        public ICollection<PaymentMethod> PaymentMethods { get; set; } = new List<PaymentMethod>();
        public ICollection<UserSession> UserSessions { get; set; } = new List<UserSession>();
        public ICollection<OtpCode> OtpCodes { get; set; } = new List<OtpCode>();

        // Profiles
        public Student? Student { get; set; }
        public Instructor? Instructor { get; set; }
        public Admin? Admin { get; set; }
    }
}
