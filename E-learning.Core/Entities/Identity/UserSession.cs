using E_learning.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.Core.Entities.Identity
{
    public  class UserSession
    {
        public Guid Id { get; set; }
        public  Guid UserId { get; set; }
        public ApplicationUser User { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
        public string? DeviceInfo { get; set; }
        public  Status IsActive { get; set; }
        public  DateTime  ExpiresAt { get; set; }
        public DateTime CreatedAt { get; set; }


    }
}
