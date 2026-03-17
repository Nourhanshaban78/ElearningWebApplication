using E_Learning.Core.Entities.Reviews;
using E_Learning.Core.Interfaces.Repositories;

namespace E_Learning.Core.Repository
{
    public interface ICertificateRepository:IGenericRepository<Certificate,int>
    {
        public Task<IReadOnlyList<Certificate>> GetByStudentIdAsync(Guid studentId, CancellationToken ct = default);
        public  Task<bool> ExistsAsync(  Guid studentId, int courseId, CancellationToken ct = default);
        public  Task<IReadOnlyList<Certificate>> GetByCourseIdAsync( int courseId, CancellationToken ct = default);
    }
}