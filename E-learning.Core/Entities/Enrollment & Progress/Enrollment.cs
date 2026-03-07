using E_learning.Core.Entities.Base;
using E_learning.Core.Entities.Billing___Payments;
using E_learning.Core.Entities.Courses___content;
using E_learning.Core.Entities.Identity;
using E_learning.Core.Entities.Profiles;
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
        public Guid StudentId { get; set; }
        public Student? Student { get; set; }

        public Guid CourseId { get; set; }
        public Courses Course { get; set; } = null!;

        public Guid? TransactionId { get; set; }
        public PaymentTransactions? Transaction { get; set; }

        public EnrollmentStatus Status { get; set; } = EnrollmentStatus.NotStarted;
        public decimal ProgressPercentage { get; set; }
        public DateTime? CompletedAt { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string? DeletedBy { get; set; }

        public ICollection<LessonProgress> LessonProgresses { get; set; } = new List<LessonProgress>();
    }
}
