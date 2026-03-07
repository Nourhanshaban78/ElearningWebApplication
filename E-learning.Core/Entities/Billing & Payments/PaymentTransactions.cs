using E_learning.Core.Entities.Courses___content;
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
    public class PaymentTransactions
    {
        public Guid Id { get; set; }

        public Guid StudentId { get; set; }
        public Student? Student { get; set; }

        public Guid CourseId { get; set; }
        public Courses? Courses { get; set; }

        public Guid PaymentMethodId { get; set; }
        public PaymentMethods? PaymentMethods { get; set; }

        public decimal Amount { get; set; }
        public string Currency { get; set; } = "USD";
        public PaymentTransactionsStatus Status { get; set; }
        public string? GatewayReference { get; set; }
        public string? FailureReason { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? CompletedAt { get; set; }

        public ICollection<InstructorEarnings> InstructorEarnings { get; set; } = new List<InstructorEarnings>();
    }
}
