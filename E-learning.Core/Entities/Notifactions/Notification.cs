using E_learning.Core.Entities.Identity;
using E_learning.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.Core.Entities.Notifactions
{
    public class Notification
    {
        public Guid Id { get; set; }
        public ApplicationUser User { get; set; } = default!;
        public string Title { get; set; } = default!;
        public string Body { get; set; } = default!;
        public NotificationType Type { get; set; } = default!;
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; } // Default GETUTCDATE()
    }
}
