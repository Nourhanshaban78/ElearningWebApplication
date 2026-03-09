using E_learning.Core.Entities.Identity;
using E_Learning.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Core.Entities.Schedule
{
    public class StudyReminder : BaseEntity
    {
        // ─── FK → AppUser ────────────────────────
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; } = null!;

        // ─── Reminder Info ───────────────────────
        public string Title { get; set; } = string.Empty;

        public TimeOnly ReminderTime { get; set; }

        public bool IsDaily { get; set; } = true;
        public DateOnly? SpecificDate { get; set; }

        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
