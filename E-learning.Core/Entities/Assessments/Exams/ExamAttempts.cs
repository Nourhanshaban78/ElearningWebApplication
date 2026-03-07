using E_learning.Core.Entities.Identity;
using E_learning.Core.Entities.Profiles;
using E_learning.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.Core.Entities.Assessments.Exams
{
    public class ExamAttempts
    {
        public Guid Id { get; set; }

        public Guid StudentId { get; set; }
        public Student Student { get; set; } = null!;

        public Guid ExamId { get; set; }
        public Exams Exams { get; set; }

        public Guid? ReviewedBy { get; set; }
        public ApplicationUser User { get; set; }

        public DateTime StartedAt { get; set; } = DateTime.UtcNow;
        public DateTime? SubmittedAt { get; set; }
        public DateTime? ReviewedAt { get; set; }
        
        public decimal? Score { get; set; }
        public bool IsPublished { get; set; } = false;
        public bool? IsPassed { get; set; }
        public string? TeacherComment { get; set; }
        public ExamAttemptsStatus Status { get; set; } = ExamAttemptsStatus.InProgress;

        public ICollection<ExamAttemptAnswers> ExamAttemptAnswers { get; set; }
    }
}
