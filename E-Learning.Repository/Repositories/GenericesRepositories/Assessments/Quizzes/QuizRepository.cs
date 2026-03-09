using E_Learning.core.Interfaces.Repositories.Assessments.Quizzes;
using E_Learning.Repository.Data;

namespace E_Learning.Repository.Repositories.GenericesRepositories.Assessments.Quizzes
{
    public class QuizRepository : IQuizRepository
    {
        public QuizRepository(ELearningDbContext context)
        {
            _context = context;
        }

        public ELearningDbContext _context { get; }
    }
}
