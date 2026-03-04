using E_learning.Core.Entities.Assessments.Quizzes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.Core.Entities.Assessments.Exams
{
    public class ExamAttemptAnswers
    {
        public Guid Id { get; set; }

        public Guid AttemptId { get; set; }
        public ExamAttempts ExamAttempts { get; set; }

        public Guid QuestionId { get; set; }
        public ExamQuestions ExamQuestions { get; set; }

        public Guid SelectedOption { get; set; }
        public ExamOptions? ExamOptions { get; set; }

        public string? TextAnswer { get; set; }
        public decimal? Score { get; set; }
        public bool? IsCorrect { get; set; }
    }
}
