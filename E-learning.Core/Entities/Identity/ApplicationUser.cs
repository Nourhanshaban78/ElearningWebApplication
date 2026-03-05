using Microsoft.AspNetCore.Identity;

using E_learning.Core.Entities.Assessments.Exams;
using E_learning.Core.Entities.Assessments.Quizzes;
using E_learning.Core.Entities.Billing___Payments;
using E_learning.Core.Entities.Courses___content;
using E_learning.Core.Enums;
using E_learning.Core.Entities.Notifactions;
using E_learning.Core.Entities.Enrollment___Progress;


namespace E_learning.Core.Entities.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FullName { get; set; } = string.Empty;
        public string? Bio { get; set; } = string.Empty;
        public string? ProfileImage { get; set; } = string.Empty;
        public string? Location { get; set; } = string.Empty;
        public DateOnly DateOfBirth { get; set; }
        
        public Status IsActive { get; set; }
        public DateTime MemberSince { get; set; }
        public DateTime UpdatedAt { get; set; }


        #region Notification & NotificationSetting NavProp

         public ICollection<Notifications> Notifications { get; set; } = new List<Notifications>();
         public NotificationSettings? NotificationSettings { get; set; }

        #endregion
        public ICollection<Courses> Courses { get; set; } = new List<Courses>();
        public ICollection<Courses> ApprovedCourses { get; set; } = new List<Courses>();

        public ICollection<ExamAttempts> ExamAttempts { get; set; } = new List<ExamAttempts>();
        public ICollection<QuizAttempts> QuizAttempts { get; set; } = new List<QuizAttempts>();
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        public ICollection<InstructorEarnings> InstructorEarnings { get; set; } = new List<InstructorEarnings>();
        public ICollection<PaymentMethods> PaymentMethods { get; set; } = new List<PaymentMethods>();
        public ICollection<PaymentTransactions> PaymentTransactions { get; set; } = new List<PaymentTransactions>();
        public ICollection<PayoutRequests> PayoutRequests { get; set; } = new List<PayoutRequests>();
        public ICollection<UserSession> UserSessions { get; set; } = new List<UserSession>();
        public ICollection<OtpCodes> OtpCodes { get; set; } = new List<OtpCodes>();

        // Preferences
        public string Language { get; set; } = "en";
        public string TimeZone { get; set; } = "UTC";
        public bool ProfileVisibility { get; set; } = true;
        public bool ShowProgressToOthers { get; set; } = true;

        // Notifications
        public bool NotifyInApp { get; set; } = true;
        public bool NotifyEmail { get; set; } = true;

    }
}
