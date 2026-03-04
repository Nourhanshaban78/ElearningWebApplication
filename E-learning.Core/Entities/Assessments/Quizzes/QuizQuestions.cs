using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.Core.Entities.Assessments.Quizzes
{
    public class QuizQuestions
    {
        public Guid Id { get; set; }

        public Guid QuizId { get; set; }
        public Quizzes Quizzes { get; set; }

        public string Text { get; set; }
        public string Type { get; set; }
        public decimal Points { get; set; } = 1;
        public bool IsAIGenerated { get; set; } = false;
        public int OrderIndex { get; set; }

        public ICollection<QuizOptions> QuizOptions { get; set; }
        public ICollection<QuizAttemptAnswers> QuizAttemptAnswers { get; set; }
    }
}
