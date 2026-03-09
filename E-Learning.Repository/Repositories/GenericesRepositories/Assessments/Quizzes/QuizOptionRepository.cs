using E_Learning.core.Interfaces.Repositories.Assessments.Quizzes;
using E_Learning.Repository.Data;

namespace E_Learning.Repository.Repositories.GenericesRepositories.Assessments.Quizzes
{
    public class QuizOptionRepository : IQuizOptionRepository
    {
        public QuizOptionRepository(ELearningDbContext context)
        {
            _context = context;
        }

        public ELearningDbContext _context { get; }
    }
}
