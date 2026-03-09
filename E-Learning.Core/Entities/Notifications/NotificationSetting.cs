using E_learning.Core.Entities.Identity;
using E_Learning.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Core.Entities.Notifications
{
    public class NotificationSetting : BaseEntity
    {
        // ─── FK - One-to-One ──────────────────────
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; } = null!;

        // ─── Types On/Off ─────────────────────────
        public bool CourseAnnouncement { get; set; } = true;
        public bool AssignmentReminder { get; set; } = true;
        public bool ExamNotification { get; set; } = true;
        public bool PlatformUpdates { get; set; } = true;

        // ─── Channels On/Off ─────────────────────
        public bool InAppNotification { get; set; } = true;
        public bool EmailNotification { get; set; } = true;
    }
}
