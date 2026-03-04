using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_learning.Core.Entities.Identity;

namespace E_learning.Core.Entities.AdminOperations
{
    public class SupportTicketReplies
    {
        public Guid Id { get; set; }

        public Guid TicketId { get; set; }
        public virtual SupportTickets Ticket { get; set; }

        public Guid SenderId { get; set; }
        public virtual ApplicationUser Sender { get; set; }

        public string Body { get; set; }= string.Empty;
        public DateTime CreatedAt { get; set; }=DateTime.UtcNow;
    }
}