using E_Learning.core.Interfaces.Repositories.Courses;
using E_Learning.Repository.Data;

namespace E_Learning.Repository.Repositories.GenericesRepositories.Courses
{
    public class SectionRepository: ISectionRepository
    {
        public SectionRepository(ELearningDbContext context)
        {
            _context = context;
        }

        public ELearningDbContext _context { get; }
    }
}
