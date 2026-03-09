using E_learning.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.Core.Entities.Assessments.Quizzes
{
    public class QuizQuestion
    {
        public Guid Id { get; set; }

        public Guid QuizId { get; set; }
        public string Text { get; set; }
        public QuizQuestionsType Type { get; set; }
        public decimal Points { get; set; } = 1;
        public bool IsAIGenerated { get; set; } = false;
        public int OrderIndex { get; set; }
        public Quiz Quiz { get; set; } = null!;
        public ICollection<QuizOption> QuizOptions { get; set; } = new List<QuizOption>();
        public ICollection<QuizAttemptAnswer> QuizAttemptAnswers { get; set; } = new List<QuizAttemptAnswer>();
      
    }
}
