using Microsoft.AspNetCore.Identity;


namespace E_learning.Core.Entities.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
        public string? Bio { get; set; }
        public string? ProfileImage { get; set; }
        public string? Location { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime MemberSince { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        //// Preferences
        //public string Language { get; set; } = "en";
        //public string TimeZone { get; set; } = "UTC";
        //public bool ProfileVisibility { get; set; } = true;
        //public bool ShowProgressToOthers { get; set; } = true;

        //// Notifications
        //public bool NotifyInApp { get; set; } = true;
        //public bool NotifyEmail { get; set; } = true;
    }
}
