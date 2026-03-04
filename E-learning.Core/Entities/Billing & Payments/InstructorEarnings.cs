using E_learning.Core.Entities.Courses___content;
using E_learning.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.Core.Entities.Billing___Payments
{
    public class InstructorEarnings
    {
        public Guid Id { get; set; }

        public Guid InstructorId { get; set; }
        public ApplicationUser Instructor { get; set; }

        public Guid TransactionId { get; set; }
        public PaymentTransactions PaymentTransactions { get; set; }

        public Guid CourseId { get; set; }
        public Courses Courses { get; set; }

        public decimal GrossAmount { get; set; }
        public decimal PlatformFee { get; set; }
        public decimal NetAmount { get; set; }
        public string Status { get; set; } = "Pending";
        public DateTime AvailableAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
