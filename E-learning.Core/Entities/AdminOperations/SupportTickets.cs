using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_learning.Core.Entities.Identity;
using E_learning.Core.Enums;

namespace E_learning.Core.Entities.AdminOperations
{
    public class SupportTickets
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; } = null!;

        public string Subject { get; set; }= string.Empty;
        public string Body { get; set; }= string.Empty;

        public TicketType Type { get; set; }
        public TicketStatus Status { get; set; }=TicketStatus.Open;

        public Guid? AssignedTo { get; set; }
        public  ApplicationUser? Assigned { get; set; }

        public DateTime CreatedAt { get; set; }=DateTime.UtcNow;
        public DateTime? ResolvedAt { get; set; }

        public ICollection<SupportTicketReplies> Replies { get; set; } = new List<SupportTicketReplies>();
    }
}