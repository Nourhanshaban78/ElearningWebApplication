using E_learning.Core.Entities.Identity;
using E_Learning.Core.Base;
using E_Learning.Core.Entities.Courses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Core.Entities.Schedule
{
    public class ScheduleEvent : BaseEntity
    {
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; } = null!;

        public int? CourseId { get; set; }
        public Course? Course { get; set; }

        // ─── Event Info ──────────────────────────
        public string Title { get; set; } = string.Empty;

        // Exam, Quiz, Assignment, Lesson, Reminder
        public string Type { get; set; } = string.Empty;

        // Low, Medium, High
        public string Priority { get; set; } = "Medium";

        public DateTime StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
