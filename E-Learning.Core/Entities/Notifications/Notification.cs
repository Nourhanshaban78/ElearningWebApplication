using E_learning.Core.Entities.Identity;
using E_Learning.Core.Base;
using E_Learning.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Core.Entities.Notifications
{

    public class Notification : BaseEntity
    {
        // ─── FK ──────────────────────────────────
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; } = null!;

        // ─── Content ─────────────────────────────
        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;

        public NotificationType Type { get; set; }
            = NotificationType.General;

        public bool IsRead { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
