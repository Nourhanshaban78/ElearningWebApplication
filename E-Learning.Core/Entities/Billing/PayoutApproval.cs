using E_learning.Core.Entities.Identity;
using E_Learning.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Core.Entities.Billing
{
    public class PayoutApproval : BaseEntity
    {
        // ─── FKs ─────────────────────────────────
        // One-to-One , PayoutRequest
        public int PayoutRequestId { get; set; }
        public PayoutRequest PayoutRequest { get; set; } = null!;

        public Guid AdminId { get; set; }
        public ApplicationUser Admin { get; set; } = null!;

        // ─── Decision ────────────────────────────
        // Approved , Rejected
        public string Decision { get; set; } = string.Empty;
        public string? Notes { get; set; }
        public DateTime ProcessedAt { get; set; } = DateTime.UtcNow;
    }
}
