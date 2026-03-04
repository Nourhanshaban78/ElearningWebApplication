using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.Core.Entities.Assessments.Exams
{
    public class ExamQuestions
    {
        public Guid Id { get; set; }

        public Guid ExamId { get; set; }
        public Exams Exams { get; set; }

        public string Text { get; set; }
        public string Type { get; set; }
        public decimal Points { get; set; }
        public bool IsAIGenerated { get; set; } = false;
        public int OrderIndex { get; set; }

        public ICollection<ExamOptions> ExamOptions { get; set; }
        public ICollection<ExamAttemptAnswers> ExamAttemptAnswers { get; set; }

    }
}
