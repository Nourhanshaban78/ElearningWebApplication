using E_learning.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.Core.Entities.Notifactions
{
    public class NotificationSettings
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public ApplicationUser? User { get; set; }

        public bool CourseAnnouncement { get; set; } = true;
        public bool AssignmentReminder { get; set; } = true;
        public bool ExamNotification { get; set; } = true;
        public bool PlatformUpdates { get; set; } = true;
        public bool InAppNotification { get; set; } = true;
        public bool EmailNotification { get; set; } = true;
    }
}
