using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.Core.Entities.Assessments.Quizzes
{
    public class QuizOptions
    {
        public Guid Id { get; set; }

        public Guid QuestionId { get; set; }
        public QuizQuestions QuizQuestions { get; set; }

        public string Text { get; set; }
        public bool IsCorrect { get; set; } = false;
        public int OrderIndex { get; set; }

        public ICollection<QuizAttemptAnswers> QuizAttemptAnswers { get; set; }
    }
}
