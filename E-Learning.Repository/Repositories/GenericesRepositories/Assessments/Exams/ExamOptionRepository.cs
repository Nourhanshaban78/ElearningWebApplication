using E_Learning.Core.Interfaces.Repositories.Assessments.Exams;
using E_Learning.Repository.Data;

namespace E_Learning.Repository.Repositories.GenericesRepositories.Assessments.Exams
{
    public class ExamOptionRepository: IExamOptionRepository
    {
        public ExamOptionRepository(ELearningDbContext context)
        {
            _context = context;
        }

        public ELearningDbContext _context { get; }
    }
}
