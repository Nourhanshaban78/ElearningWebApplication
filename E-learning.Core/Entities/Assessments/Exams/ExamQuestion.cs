using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.Core.Entities.Assessments.Exams
{
    public class ExamQuestion
    {
        public Guid Id { get; set; }

        public Guid ExamId { get; set; }
        public Exam Exam { get; set; } = null!;
        public string Text { get; set; }
        public string Type { get; set; }
        public decimal Points { get; set; }
        public bool IsAIGenerated { get; set; } = false;
        public int OrderIndex { get; set; }
       
        public ICollection<ExamOption> ExamOptions { get; set; } = new List<ExamOption>();
        public ICollection<ExamAttemptAnswer> ExamAttemptAnswers { get; set; } = new List<ExamAttemptAnswer>();
        

    }
}
