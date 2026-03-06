using E_learning.Core.Entities.Courses___content;
using E_learning.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.Core.Entities.Assessments.Quizzes
{
    public class Quizzes
    {
        public Guid Id { get; set; }

        public Guid CourseId { get; set; }
        public Courses Courses { get; set; }

        public Guid? LessonId { get; set; }
        public Lessons Lessons { get; set; }

        public string Title { get; set; }
        public string? Topic { get; set; }
        public QuizzesType Type { get; set; } = QuizzesType.Regular;
        public int? TimeLimitSeconds { get; set; }
        public int TimePerQuestionSeconds { get; set; } = 30;
        public decimal PassingScore { get; set; } = 60;
        public int MaxAttempts { get; set; }

        public bool ShuffleQuestions { get; set; } = true;
        public bool ShowResultsImmediately { get; set; } = true;
        public bool IsActive { get; set; } = true;

        public ICollection<QuizQuestions> QuizQuestions { get; set; }
        public ICollection<QuizAttempts> QuizAttempts { get; set; }

    }
}
