using E_learning.Core.Entities.Identity;
using E_Learning.Core.Base;
using E_Learning.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Core.Entities.Assessments.Quiz
{
    public class QuizAttempt : BaseEntity
    {
        public Guid StudentId { get; set; }
        public ApplicationUser Student { get; set; } = null!;

        public int QuizId { get; set; }
        public Quiz Quiz { get; set; } = null!;

        public DateTime StartedAt { get; set; } = DateTime.UtcNow;
        public DateTime? SubmittedAt { get; set; }
        public decimal? Score { get; set; }
        public bool? IsPassed { get; set; }
        public QuizAttemptStatus Status { get; set; }
            = QuizAttemptStatus.InProgress;

        public ICollection<QuizAttemptAnswer> Answers { get; set; }
            = new List<QuizAttemptAnswer>();
    }
}
