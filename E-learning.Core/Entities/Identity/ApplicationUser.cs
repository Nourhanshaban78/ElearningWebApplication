using E_learning.Core.Entities.Notifactions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.Core.Entities.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;
        public string ProfileImage { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public DateOnly DateOfBirth { get; set; }
        //public Status IsActive { get; set; }
        public DateTime MemberSince { get; set; }
        public DateTime UpdatedAt { get; set; }


        #region Notification & NotificationSetting NavProp

        // public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
        // public NotificationSettings? NotificationSettings { get; set; }

        #endregion
    }
}
