using E_Learning.Core.Interfaces.Repositories.Profile;
using E_Learning.Repository.Data;

namespace E_Learning.Repository.Repositories.GenericesRepositories.Profile
{
    public class StudentProfileRepository : IStudentProfileRepository
    {
        public StudentProfileRepository(ELearningDbContext context)
        {
            _context = context;
        }

        public ELearningDbContext _context { get; }
    }
}
