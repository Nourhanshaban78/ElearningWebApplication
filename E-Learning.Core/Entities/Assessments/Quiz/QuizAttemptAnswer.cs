using E_Learning.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Core.Entities.Assessments.Quiz
{
    public class QuizAttemptAnswer : BaseEntity
    {
        public int AttemptId { get; set; }
        public QuizAttempt Attempt { get; set; } = null!;

        public int QuestionId { get; set; }
        public QuizQuestion Question { get; set; } = null!;

        public int? SelectedOptionId { get; set; }
        public QuizOption? SelectedOption { get; set; }

        public string? TextAnswer { get; set; }
        public bool? IsCorrect { get; set; }
    }
}
