using E_Learning.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Core.Entities.Assessments.Exams
{
    public class ExamAttemptAnswer : BaseEntity
    {
        public int AttemptId { get; set; }
        public ExamAttempt Attempt { get; set; } = null!;

        public int QuestionId { get; set; }
        public ExamQuestion Question { get; set; } = null!;

        public int? SelectedOptionId { get; set; }
        public ExamOption? SelectedOption { get; set; }

        public string? TextAnswer { get; set; }
        public decimal? Score { get; set; }
        public bool? IsCorrect { get; set; }
    }
}
