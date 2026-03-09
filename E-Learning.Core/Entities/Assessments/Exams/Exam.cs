using E_Learning.Core.Base;
using E_Learning.Core.Entities.Courses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Core.Entities.Assessments.Exams
{
    public class Exam : AuditableEntity
    {
        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;

        public string Title { get; set; } = string.Empty;
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
        public string? SourceFileUrl { get; set; }
        public bool IsActive { get; set; } = true;

        public ICollection<ExamQuestion> Questions { get; set; }
            = new List<ExamQuestion>();
        public ICollection<ExamAttempt> Attempts { get; set; }
            = new List<ExamAttempt>();
    }
}
