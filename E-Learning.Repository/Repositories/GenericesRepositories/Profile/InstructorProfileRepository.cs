using E_Learning.Core.Interfaces.Repositories.Profile;
using E_Learning.Repository.Data;

namespace E_Learning.Repository.Repositories.GenericesRepositories.Profile
{
    public class InstructorProfileRepository :IInstructorProfileRepository
    {
        public InstructorProfileRepository(ELearningDbContext context)
        {
            _context = context;
        }

        public ELearningDbContext _context { get; }
    }
}
