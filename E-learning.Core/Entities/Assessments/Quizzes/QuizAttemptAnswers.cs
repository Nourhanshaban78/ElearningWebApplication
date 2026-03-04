using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.Core.Entities.Assessments.Quizzes
{
    public class QuizAttemptAnswers
    {
        public Guid Id { get; set; }

        public Guid AttemptId { get; set; }
        public QuizAttempts QuizAttempts { get; set; }

        public Guid QuestionId { get; set; }
        public QuizQuestions QuizQuestions { get; set; }

        public Guid SelectedOption { get; set; }
        public QuizOptions? QuizOptions { get; set; }

        public string? TextAnswer { get; set; }
        public bool? IsCorrect { get; set; }
    }
}
