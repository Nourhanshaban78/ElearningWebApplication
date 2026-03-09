using E_Learning.Core.Interfaces.Repositories.Payments;
using E_Learning.Repository.Data;

namespace E_Learning.Repository.Repositories.GenericesRepositories.Payments
{
    public class InstructorEarningRepository : IInstructorEarningRepository
    {
        public InstructorEarningRepository(ELearningDbContext context)
        {
            _context = context;
        }

        public ELearningDbContext _context { get; }
    }
}
