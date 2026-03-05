using E_learning.Core.Entities.Base;
using E_learning.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.Core.Entities.LiveSessions
{
    public class LiveSessionAttendee : BaseEntity
    {
        // ─── Foreign Keys ───────────────────────
        public int SessionId { get; set; }
        public LiveSession Session { get; set; } = null!;

        public Guid StudentId { get; set; }
        public ApplicationUser Student { get; set; } = null!;

        // ─── Attendance ──────────────────────────

        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
        public DateTime? LeftAt { get; set; }

        public int? DurationSeconds { get; set; }
    }
}
