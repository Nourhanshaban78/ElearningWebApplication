using E_learning.Core.Entities.Courses___content;
using E_learning.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.Core.Entities.Profiles

{
    public class Instructor
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }

        public string? Headline { get; set; }
        public string? About { get; set; }

        public decimal TotalEarnings { get; set; } = 0;
        public decimal PendingPayout { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Courses> Courses { get; set; }
        //ublic ICollection<livesession> LiveSessions { get; set; }

    }
}
