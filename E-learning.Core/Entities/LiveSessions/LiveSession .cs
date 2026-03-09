using E_learning.Core.Entities.Base;
using E_learning.Core.Entities.Courses___content;
using E_learning.Core.Entities.Identity;
using E_learning.Core.Entities.Profiles;
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
        public Instructor Instructor { get; set; } = null!;

        public Guid CourseId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public DateTime ScheduledAt { get; set; }

        public int DurationMinutes { get; set; } = 60;

        public DateTime EndAt => ScheduledAt.AddMinutes(DurationMinutes);

        public string MeetingLink { get; set; } = string.Empty;

        public string? RecordingUrl { get; set; }

        public bool IsRecorded { get; set; } = true;

        public bool IsVisibleToStudents { get; set; } = false;

        public LiveSessionStatus Status { get; set; } = LiveSessionStatus.Scheduled;

        public Course Course { get; set; } = null!;
        public ICollection<LiveSessionAttendee> Attendees { get; set; }
            = new List<LiveSessionAttendee>();
    }

}
