using E_learning.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.Core.Entities.Assessments.Quizzes
{
    public class QuizAttempts
    {
        public Guid Id { get; set; }

        public Guid StudentId { get; set; }
        public ApplicationUser Students { get; set; }

        public Guid QuizId { get; set; }
        public Quizzes Quizzes { get; set; }

        public DateTime StartedAt { get; set; } = DateTime.UtcNow;
        public DateTime? SubmittedAt { get; set; }
        public decimal? Score { get; set; }
        public bool? IsPassed { get; set; }
        public string Status { get; set; } = "InProgress";

        public ICollection<QuizAttemptAnswers> QuizAttemptAnswers { get; set; }
    }
}
