using E_learning.Core.Entities.Courses___content;
using E_learning.Core.Entities.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.Core.Entities.Assessments.Exams
{
    public class Exam
    {
        public Guid Id { get; set; }

        public Guid CourseId { get; set; }
        public Course Course { get; set; } = null!;
        public Guid InstructorId { get; set; }
        public Instructor Instructor { get; set; }
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

        public ICollection<ExamQuestion> ExamQuestions { get; set; } = new List<ExamQuestion>();
        public ICollection<ExamAttempt> ExamAttempts { get; set; } = new List<ExamAttempt>();

    }
}
