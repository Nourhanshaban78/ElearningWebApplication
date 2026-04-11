using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace E_Learning.Service.DTOs.Profiles.Admin
{
    public class UpdateAdminProfileDto
    {
        public string? FullName { get; set; }

        public string? PhoneNumber { get; set; }
     
        public IFormFile? ProfilePicture { get; set; }

        // ضيفي هدول السطرين عشان يختفي الخطأ
        public string? Role { get; set; }
        public string? Password { get; set; }
    }
}