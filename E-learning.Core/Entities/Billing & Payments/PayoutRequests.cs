using E_learning.Core.Entities.Identity;
using E_learning.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.Core.Entities.Billing___Payments
{
    public class PayoutRequests
    {
        public Guid Id { get; set; }

        public Guid InstructorId { get; set; }
        public ApplicationUser Instructor { get; set; }

        public decimal Amount { get; set; }
        public string Method { get; set; }
        public string? AccountDetails { get; set; }
        public Status Status { get; set; } 
        public DateTime RequestedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ProcessedAt { get; set; }
        public string? AdminNotes { get; set; }
    }
}
