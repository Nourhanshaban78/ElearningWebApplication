using E_Learning.core.Interfaces.Repositories.Assessments.Exams;
using E_Learning.Repository.Data;

namespace E_Learning.Repository.Repositories.GenericesRepositories.Assessments.Exams
{
    public class ExamAttemptRepository : IExamAttemptRepository
    {
        public ExamAttemptRepository(ELearningDbContext context)
        {
            _context = context;
        }

        public ELearningDbContext _context { get; }
    }
}
