using E_Learning.Core.Interfaces.Repositories.Payments;
using E_Learning.Repository.Data;

namespace E_Learning.Repository.Repositories.GenericesRepositories.Payments
{
    public class PaymentTransactionRepository: IPaymentTransactionRepository
    {
        public PaymentTransactionRepository(ELearningDbContext context)
        {
            _context = context;
        }

        public ELearningDbContext _context { get; }
    }
}
