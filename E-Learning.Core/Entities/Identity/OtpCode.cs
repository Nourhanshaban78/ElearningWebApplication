using E_learning.Core.Entities.Identity;
using E_Learning.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Core.Entities.Identity
{
    public class OtpCode : BaseEntity
    {
        // ─── FK → AppUser ────────────────────────
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; } = null!;

        // ─── Code ────────────────────────────────
        public string Code { get; set; } = string.Empty;

        // ─── Purpose ─────────────────────────────
        public string Purpose { get; set; } = string.Empty;

        // ─── Status ──────────────────────────────
        public bool IsUsed { get; set; } = false;

        public DateTime ExpiresAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
