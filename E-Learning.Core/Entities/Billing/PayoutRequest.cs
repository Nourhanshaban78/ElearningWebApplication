using E_learning.Core.Entities.Identity;
using E_Learning.Core.Base;
using E_Learning.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Core.Entities.Billing
{
    public class PayoutRequest : BaseEntity
    {
        // ─── FK ──────────────────────────────────
        public Guid InstructorId { get; set; }
        public ApplicationUser Instructor { get; set; } = null!;

        // ─── Request Info ────────────────────────
        public decimal Amount { get; set; }

        // BankTransfer, PayPal, Stripe
        public string Method { get; set; } = string.Empty;

        //Encrypted
        public string? AccountDetails { get; set; }

        // ─── Status ──────────────────────────────
        public PayoutStatus Status { get; set; }
            = PayoutStatus.Pending;

        public DateTime RequestedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ProcessedAt { get; set; }

        public string? AdminNotes { get; set; }

        // ─── Navigation ──────────────────────────
        public PayoutApproval? PayoutApproval { get; set; }
    }
}
