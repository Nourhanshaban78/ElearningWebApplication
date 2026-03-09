using E_learning.Core.Entities.Assessments.Quizzes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.Core.Entities.Assessments.Exams
{
    public class ExamOption
    {
        public Guid Id { get; set; }

        public Guid QuestionId { get; set; }
        public ExamQuestion ExamQuestion { get; set; } = null!;
        public string Text { get; set; }
        public bool IsCorrect { get; set; } = false;
        public int OrderIndex { get; set; }
      
        public ICollection<ExamAttemptAnswer> ExamAttemptAnswers { get; set; } = new List<ExamAttemptAnswer>();
    }
}
