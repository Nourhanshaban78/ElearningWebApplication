using E_learning.Core.Entities.AdminOperations;
using E_learning.Core.Entities.Identity;
using E_learning.Core.Entities.Profiles;
using E_learning.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.Core.Entities.Billing___Payments
{
    public class PayoutRequest
    {
        public Guid Id { get; set; }

        public Guid InstructorId { get; set; }
        public Instructor Instructor { get; set; } = null!;


        public decimal Amount { get; set; }
        public string Method { get; set; }
        public string? AccountDetails { get; set; }
        public PaymentTransactionsStatus Status { get; set; } = PaymentTransactionsStatus.Pending; 
        public DateTime RequestedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ProcessedAt { get; set; }
        public string? AdminNotes { get; set; }
        public ICollection<PayoutApprovals> PayoutApprovals { get; set; } = new List<PayoutApprovals>();
    }
}
