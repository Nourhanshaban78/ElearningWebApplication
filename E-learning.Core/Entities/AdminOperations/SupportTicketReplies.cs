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
        public SupportTickets Ticket { get; set; } = null!;

        public Guid SenderId { get; set; }
        public ApplicationUser Sender { get; set; } = null!;

        public string Body { get; set; }= string.Empty;
        public DateTime CreatedAt { get; set; }=DateTime.UtcNow;
        public virtual ICollection<SupportTicketReplies> Replies { get; set; } = new List<SupportTicketReplies>();
        
    }
}