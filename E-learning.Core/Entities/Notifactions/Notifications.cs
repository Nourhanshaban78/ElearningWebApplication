using E_learning.Core.Entities.Identity;
using E_learning.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.Core.Entities.Notifactions
{
    public class Notifications
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public ApplicationUser? User { get; set; }

        public string Title { get; set; }
        public string Body { get; set; }
        public NotificationType Type { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
