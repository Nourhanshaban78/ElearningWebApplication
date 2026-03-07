using E_learning.Core.Entities.Billing___Payments;
using E_learning.Core.Entities.Courses___content;
using E_learning.Core.Entities.Identity;
using E_learning.Core.Entities.LiveSessions;
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
        public ApplicationUser? User { get; set; }

        public string? Headline { get; set; }
        public string? About { get; set; }

        public decimal TotalEarnings { get; set; }
        public decimal PendingPayout { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Courses> Courses { get; set; } = new List<Courses>();
        public ICollection<InstructorEarnings> InstructorEarnings { get; set; } = new List<InstructorEarnings>();
        public ICollection<LiveSession> LiveSessions { get; set; } = new List<LiveSession>();
    }
}
