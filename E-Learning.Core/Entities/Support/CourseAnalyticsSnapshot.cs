using E_Learning.Core.Base;
using E_Learning.Core.Entities.Courses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Core.Entities.Support
{
    public class CourseAnalyticsSnapshot : BaseEntity
    {
        // ─── FK ──────────────────────────────────
        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;

        // ─── Snapshot Date ───────────────────────
        // background Job
        public DateOnly SnapshotDate { get; set; }

        // ─── Metrics ─────────────────────────────
        public int TotalStudents { get; set; } = 0;

        // last 7 days
        public int ActiveStudents { get; set; } = 0;

        public int NewEnrollments { get; set; } = 0;

        public decimal? CompletionRate { get; set; }

        public decimal? AverageGrade { get; set; }

        public decimal? QuizPassRate { get; set; }

        public decimal? ExamPassRate { get; set; }

        public decimal TotalRevenue { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
