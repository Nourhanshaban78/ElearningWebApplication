using E_Learning.Core.Interfaces.Repositories.Payments;
using E_Learning.Repository.Data;

namespace E_Learning.Repository.Repositories.GenericesRepositories.Payments
{
    public class PayoutRequestRepository: IPayoutRequestRepository
    {
        public PayoutRequestRepository(ELearningDbContext context)
        {
            _context = context;
        }

        public ELearningDbContext _context { get; }
    }
}
