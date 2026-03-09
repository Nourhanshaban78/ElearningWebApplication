using E_Learning.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Core.Entities.Assessments.Exams
{
    public class ExamQuestion : BaseEntity
    {
        public int ExamId { get; set; }
        public Exam Exam { get; set; } = null!;

        public string Text { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public decimal Points { get; set; }
        public bool IsAIGenerated { get; set; } = false;
        public int OrderIndex { get; set; }

        public ICollection<ExamOption> Options { get; set; }
            = new List<ExamOption>();
    }
}
