using E_learning.Core.Entities.Assessments.Quizzes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.Core.Entities.Assessments.Exams
{
    public class ExamAttemptAnswer
    {
        public Guid Id { get; set; }

        public Guid AttemptId { get; set; }
        public ExamAttempt ExamAttempt { get; set; }

        public Guid QuestionId { get; set; }
        public ExamQuestion ExamQuestion { get; set; }

        public Guid SelectedOptionId { get; set; }
        public ExamOption? ExamOption { get; set; }

        public string? TextAnswer { get; set; }
        public decimal? Score { get; set; }
        public bool? IsCorrect { get; set; }
    }
}
