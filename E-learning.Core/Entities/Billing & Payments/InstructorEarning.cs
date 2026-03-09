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
    public class InstructorEarning
    {
        public Guid Id { get; set; }

        public Guid InstructorId { get; set; }
        public Instructor Instructor { get; set; } = null!;

       
        public ICollection<PaymentTransaction> PaymentTransactions { get; set; }
        = new List<PaymentTransaction>();

        public Guid CourseId { get; set; }
        public Course Course { get; set; } = null!;

        public decimal GrossAmount { get; set; }
        public decimal PlatformFee { get; set; }
        public decimal NetAmount { get; set; }
        public InstructorEarningsStatus Status { get; set; } = InstructorEarningsStatus.Pending;
        public DateTime AvailableAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
