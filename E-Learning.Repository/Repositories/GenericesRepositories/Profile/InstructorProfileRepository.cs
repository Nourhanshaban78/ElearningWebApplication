using E_Learning.Core.Entities.Notifications;
using E_Learning.Core.Entities.Profiles;
using E_Learning.Core.Interfaces.Repositories.Profile;
using E_Learning.Repository.Data;
using Microsoft.EntityFrameworkCore;

namespace E_Learning.Repository.Repositories.GenericesRepositories.Profile
{
    public class InstructorProfileRepository : GenericRepository<InstructorProfile, Guid>, IInstructorProfileRepository
    {
        private readonly ELearningDbContext _context;
        public InstructorProfileRepository(ELearningDbContext context) : base(context)
        {
            _context = context;
        }

        async public Task<InstructorProfile?> GetInstructorProfileWithUserByUserIdAsync(Guid userId)
        {
            return await _context.InstructorProfiles
         .Include(i => i.AppUser)
         .FirstOrDefaultAsync(i => i.AppUserId == userId);
        }

        public async Task<InstructorProfile?> GetProfileByUserIdAsync(Guid userId)
            => await _context.InstructorProfiles.Include(o => o.AppUser).FirstOrDefaultAsync(p => p.AppUserId == userId);



    }
}
