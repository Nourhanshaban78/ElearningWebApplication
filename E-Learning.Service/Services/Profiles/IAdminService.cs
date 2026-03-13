using E_Learning.Core.Entities.Profiles;
using E_Learning.Service.DTOs.Profiles.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Service.Services.Profiles
{
    public interface IAdminService
    {
        Task<AdminProfileResponseDto> GetAdminProfile(Guid userId);
        Task<AdminProfileResponseDto> CreateAdminProfile(Guid userId, CreateAdminProfileDTo dto );
        Task<AdminProfileResponseDto> UpdateAdminProfile(Guid userId, CreateAdminProfileDTo dto);
        Task<bool> AdminProfileExists(Guid userId);
        Task<IEnumerable<AdminProfileResponseDto>> GetAllAdmins();


    }
}
