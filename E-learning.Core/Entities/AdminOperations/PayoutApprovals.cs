using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_learning.Core.Entities.Billing___Payments;
using E_learning.Core.Entities.Identity;
using E_learning.Core.Entities.Profiles;
using E_learning.Core.Enums;

namespace E_learning.Core.Entities.AdminOperations
{
    public class PayoutApprovals
    {

        public Guid Id { get; set; }

        public Guid PayoutRequestId { get; set; }
        public PayoutRequest PayoutRequest { get; set; } = null!;

        public Guid AdminId { get; set; }
        public Admin Admin { get; set; } = null!;

        public Decision Decision { get; set; }
        public string? Notes { get; set; }
        public DateTime ProcessedAt { get; set; } = DateTime.UtcNow;
    }
}