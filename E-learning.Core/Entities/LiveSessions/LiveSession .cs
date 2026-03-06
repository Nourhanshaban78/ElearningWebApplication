using E_learning.Core.Entities.Base;
using E_learning.Core.Entities.Identity;
using E_learning.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.Core.Entities.LiveSessions
{
    public class LiveSession : AuditableEntity
    {

        public Guid InstructorId { get; set; }
        public ApplicationUser Instructor { get; set; } = null!;

        public int CourseId { get; set; }

        // ─── Basic Info ──────────────────────────

        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }

        // ─── Schedule ───────────────────────────

        public DateTime ScheduledAt { get; set; }
        public int DurationMinutes { get; set; } = 60;

        public DateTime EndAt => ScheduledAt.AddMinutes(DurationMinutes);

        // ─── Meeting ────────────────────────────

        public string MeetingLink { get; set; } = string.Empty;
        public string? RecordingUrl { get; set; }
        public bool IsRecorded { get; set; } = true;

        // ─── Visibility ─────────────────────────

        public bool IsVisibleToStudents { get; set; } = false;

        // ─── Status ─────────────────────────────

        public LiveSessionStatus Status { get; set; }
            = LiveSessionStatus.Scheduled;

        // ─── Navigation ─────────────────────────
        public ICollection<LiveSessionAttendee> Attendees { get; set; }
            = new List<LiveSessionAttendee>();
    }
    
}
