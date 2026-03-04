using E_learning.Core.Entities.Courses___content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_learning.Core.Entities
{
    public class CourseAnalyticsSnapshots
    {
        public Guid Id { get; set; }

        public Guid CourseId { get; set; }
        public virtual Courses Course { get; set; } = new Courses();

        public DateTime SnapshotDate { get; set; }
        public int TotalStudents { get; set; } = 0;
        public int ActiveStudents { get; set; } = 0;

        public decimal? AverageGrade { get; set; }
        public decimal? CompletionRate { get; set; }
        public decimal? AvgWeeklyHours { get; set; }

        public int NewEnrollments { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}