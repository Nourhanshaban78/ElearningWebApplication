using E_learning.Core.Entities.Courses___content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.Core.Entities.Assessments.Exams
{
    public class Exams
    {
        public Guid Id { get; set; }

        public Guid CourseId { get; set; }
        public Courses Courses { get; set; }

        public string Title { get; set; }
        public string? Instructions { get; set; }
        public string? Rules { get; set; }
        public string? TechnicalRequirements { get; set; }
        public string? EducationLevel { get; set; }
        public DateTime ScheduledAt { get; set; }
        public DateTime? EndDateTime { get; set; }
        public int DurationSeconds { get; set; }
        public decimal TotalMarks { get; set; }
        public decimal PassingScore { get; set; } = 60;
        public int MaxAttempts { get; set; } = 1;
        public bool AIShuffleEnabled { get; set; } = true;
        public string SourceFileUrl { get; set; }
        public bool IsActive { get; set; } = true;

        public ICollection<ExamQuestions> ExamQuestions { get; set; }
        public ICollection<ExamAttempts> ExamAttempts { get; set; }

    }
}
