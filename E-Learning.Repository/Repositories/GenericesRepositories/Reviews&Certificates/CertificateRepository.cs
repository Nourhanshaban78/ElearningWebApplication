using E_Learning.Core.Repository;
using E_Learning.Repository.Data;

namespace E_Learning.Repository.Repositories.GenericesRepositories.Reviews_Certificates
{
    public class CertificateRepository : ICertificateRepository
    {
        public CertificateRepository(ELearningDbContext context)
        {
            _context = context;
        }

        public ELearningDbContext _context { get; }
    }
}
