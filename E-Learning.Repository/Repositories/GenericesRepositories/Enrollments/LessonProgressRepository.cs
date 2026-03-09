using E_Learning.Core.Interfaces.Repositories.Enrollments;
using E_Learning.Repository.Data;

namespace E_Learning.Repository.Repositories.GenericesRepositories.Enrollments
{
    public class LessonProgressRepository: ILessonProgressRepository
    {
        public LessonProgressRepository(ELearningDbContext context)
        {
            _context = context;
        }

        public ELearningDbContext _context { get; }
    }
}
