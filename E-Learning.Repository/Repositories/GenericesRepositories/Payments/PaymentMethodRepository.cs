using E_Learning.Core.Interfaces.Repositories.Payments;
using E_Learning.Repository.Data;

namespace E_Learning.Repository.Repositories.GenericesRepositories.Payments
{
    public class PaymentMethodRepository: IPaymentMethodRepository
    {
        public PaymentMethodRepository(ELearningDbContext context)
        {
            _context = context;
        }

        public ELearningDbContext _context { get; }
    }
}
