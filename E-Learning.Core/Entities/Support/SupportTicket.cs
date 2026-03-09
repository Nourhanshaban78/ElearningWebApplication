using E_learning.Core.Entities.Identity;
using E_Learning.Core.Base;
using E_Learning.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Core.Entities.Support
{
    public class SupportTicket : AuditableEntity
    {
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; } = null!;

        public Guid? AssignedTo { get; set; }
        public ApplicationUser? AssignedAdmin { get; set; }

        // ─── Ticket Info ─────────────────────────
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;

        // Technical, Billing, Academic, General
        public string Type { get; set; } = string.Empty;

        // Open, InProgress, Resolved, Closed
        public SupportTicketStatus Status { get; set; }
            = SupportTicketStatus.Open;

        public DateTime? ResolvedAt { get; set; }

        // ─── Navigation ──────────────────────────
        public ICollection<SupportTicketReply> Replies { get; set; }
            = new List<SupportTicketReply>();
    }
}
