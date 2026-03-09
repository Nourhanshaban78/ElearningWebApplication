using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.Core.Entities.Assessments.Quizzes
{
    public class QuizAttemptAnswer
    {
        public Guid Id { get; set; }

        public Guid AttemptId { get; set; }
        public QuizAttempt QuizAttempt { get; set; } = null!;

        public Guid QuestionId { get; set; }
        public QuizQuestion QuizQuestion { get; set; } = null!;

        public Guid? SelectedOptionId { get; set; }
        public QuizOption? QuizOption { get; set; }

        public string? TextAnswer { get; set; }
        public bool? IsCorrect { get; set; }
    }
}
