using E_learning.Core.Entities.Identity;
using E_Learning.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Core.Entities.Identity
{
    public class UserSession : BaseEntity
    {
        // ─── FK → AppUser ────────────────────────
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; } = null!;

        public string RefreshToken { get; set; } = string.Empty;

        // ─── Device Info ─────────────────────────
     
        public string? DeviceInfo { get; set; }
        public string? IpAddress { get; set; }

        // ─── Status ──────────────────────────────
        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime ExpiresAt { get; set; }
        public DateTime LastActivityAt { get; set; } = DateTime.UtcNow;
    }
}
