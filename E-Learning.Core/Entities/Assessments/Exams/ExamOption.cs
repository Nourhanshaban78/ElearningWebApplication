using E_Learning.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Core.Entities.Assessments.Exams
{
    public class ExamOption : BaseEntity
    {
        public int QuestionId { get; set; }
        public ExamQuestion Question { get; set; } = null!;

        public string Text { get; set; } = string.Empty;
        public bool IsCorrect { get; set; } = false;
        public int OrderIndex { get; set; }
    }
}
