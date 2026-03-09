using E_learning.Core.Entities.Identity;
using E_Learning.Core.Base;
using E_Learning.Core.Entities.Courses;
using E_Learning.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Core.Entities.Billing
{
    public class InstructorEarning : BaseEntity
    {
        public Guid InstructorId { get; set; }
        public ApplicationUser Instructor { get; set; } = null!;

        // One-to-One PaymentTransaction
        public int TransactionId { get; set; }
        public PaymentTransaction Transaction { get; set; } = null!;

        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;

        // ─── Financial ───────────────────────────
        public decimal GrossAmount { get; set; }
        // % platform fee 
        public decimal PlatformFee { get; set; }

        public decimal NetAmount { get; set; }

        // ─── Status ──────────────────────────────
        // Pending → Available (after  Hold Period) → PaidOut
        public EarningStatus Status { get; set; }
            = EarningStatus.Pending;

        //  (Hold Period)
        public DateTime? AvailableAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
