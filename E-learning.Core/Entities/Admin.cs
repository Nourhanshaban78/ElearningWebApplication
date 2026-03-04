using E_learning.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.Core.Entities
{
    public class Admin
    {
        public int AdminId { get; set; }

        public string ApplicationUserId { get; set; }
        public ApplicationUser User { get; set; }

        public bool IsSuperAdmin { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
