using E_Learning.Core.Entities.Profiles;
using E_Learning.Core.Interfaces.Repositories.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Service.Services.Profiles
{
    public class AdminService
    {
        private readonly IAdminProfileRepository _adminProfileRepository;
        public AdminService(IAdminProfileRepository adminProfileRepository) {
            _adminProfileRepository = adminProfileRepository;

        }
        public async Task<AdminProfile> GetAdminProfile(Guid userId)
        {
            return await _adminProfileRepository.GetAdminProfileWithUserByUserIdAsync(userId);
        }
        public async Task<AdminProfile>
    }
}
