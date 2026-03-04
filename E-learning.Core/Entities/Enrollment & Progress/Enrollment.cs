using E_learning.Core.Entities.Base;
using E_learning.Core.Entities.Billing___Payments;
using E_learning.Core.Entities.Courses___content;
using E_learning.Core.Entities.Identity;
using E_learning.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.Core.Entities.Enrollment___Progress
{
    public class Enrollment : AuditableEntity, ISoftDelete
    {
        
        public string StudentId { get; set; }
        public ApplicationUser Student { get; set; } = null!;

        public int CourseId { get; set; }
        public Courses Course { get; set; } = null!;

        public int? TransactionId { get; set; }
        public PaymentTransactions? Transaction { get; set; }

        public EnrollmentStatus Status { get; set; } = EnrollmentStatus.NotStarted;

        public decimal ProgressPercentage { get; set; } = 0;

        public DateTime? CompletedAt { get; set; }

        // ─── ISoftDelete ────────────────────────
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }
        public string? DeletedBy { get; set; }

        // Navigation
        public ICollection<LessonProgress> LessonProgresses { get; set; }
            = new List<LessonProgress>();

    }
}
