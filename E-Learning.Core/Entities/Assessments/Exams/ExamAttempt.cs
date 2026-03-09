using E_learning.Core.Entities.Identity;
using E_Learning.Core.Base;
using E_Learning.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Core.Entities.Assessments.Exams
{
    public class ExamAttempt : BaseEntity
    {
        public Guid StudentId { get; set; }
        public ApplicationUser Student { get; set; } = null!;

        public int ExamId { get; set; }
        public Exam Exam { get; set; } = null!;

        public DateTime StartedAt { get; set; } = DateTime.UtcNow;
        public DateTime? SubmittedAt { get; set; }
        public decimal? Score { get; set; }
        public bool? IsPassed { get; set; }

        // Admin Revie
        public DateTime? ReviewedAt { get; set; }
        public Guid? ReviewedBy { get; set; }
        public ApplicationUser ReviewedByUser { get; set; }
        public string? ReviewDecision { get; set; }
        public bool IsPublished { get; set; } = false;
        public string? TeacherComment { get; set; }

        public ExamAttemptStatus Status { get; set; }
            = ExamAttemptStatus.InProgress;

        public ICollection<ExamAttemptAnswer> Answers { get; set; }
            = new List<ExamAttemptAnswer>();
    }
}
