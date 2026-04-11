using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Learning.Service.DTOs.User
{
    public class UpdateUserDto
    {
          public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? phoneNumber { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string? Level { get; set; }

    }
}