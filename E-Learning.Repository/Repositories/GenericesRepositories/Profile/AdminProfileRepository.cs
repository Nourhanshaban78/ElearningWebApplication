using E_Learning.Core.Interfaces.Repositories.Profile;
using E_Learning.Repository.Data;

namespace E_Learning.Repository.Repositories.GenericesRepositories.Profile
{
    public class AdminProfileRepository :IAdminProfileRepository
    {
        public ELearningDbContext _context { get; }
        public AdminProfileRepository(ELearningDbContext context)
        {
            _context = context;
        }
    }
}
