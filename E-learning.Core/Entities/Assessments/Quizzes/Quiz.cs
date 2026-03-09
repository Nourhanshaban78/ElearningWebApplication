using E_learning.Core.Entities.Courses___content;
using E_learning.Core.Entities.Profiles;
using E_learning.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.Core.Entities.Assessments.Quizzes
{
    public class Quiz
    {
        public Guid Id { get; set; }

        public Guid CourseId { get; set; }
        public Course Course { get; set; } = null!;
        public Guid? LessonId { get; set; }
        public Guid InstructorId { get; set; }
         public Instructor Instructor { get; set; } = null!;
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

      
        public Lesson? Lesson { get; set; }
      

        public ICollection<QuizQuestion> QuizQuestions { get; set; } = new List<QuizQuestion>();
        public ICollection<QuizAttempt> QuizAttempts { get; set; } = new List<QuizAttempt>();

    }
}
