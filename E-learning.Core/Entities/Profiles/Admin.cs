using E_learning.Core.Entities.AdminOperations;
using E_learning.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.Core.Entities.Profiles

{
    public class Admin
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public ApplicationUser? User { get; set; }

        public bool IsSuperAdmin { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<PayoutApprovals> PayoutApprovals { get; set; } = new List<PayoutApprovals>();
    }
}
