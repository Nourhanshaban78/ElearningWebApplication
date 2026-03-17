using E_Learning.Core.Entities.Reviews;
using E_Learning.Core.Repository;
using E_Learning.Repository.Data;
using Microsoft.EntityFrameworkCore;

namespace E_Learning.Repository.Repositories.GenericesRepositories.Reviews_Certificates
{
    public class CertificateRepository : GenericRepository<Certificate, int>, ICertificateRepository
    {
        private readonly ELearningDbContext _context;

        public CertificateRepository(ELearningDbContext context) : base(context)
        {
            _context = context;
        }

        // ✅ List مش single — الطالب ممكن عنده certificates في courses مختلفة
        public async Task<IReadOnlyList<Certificate>> GetByStudentIdAsync(
            Guid studentId, CancellationToken ct = default)
        {
            return await _context.Certificates
                .Where(c => c.StudentId == studentId)
                .Include(c => c.Course)
                .Include(c => c.Student)
                .OrderByDescending(c => c.IssuedAt)
                .AsNoTracking()
                .ToListAsync(ct);
        }

        // ✅ كل الـ certificates بتاعة course معين
        public async Task<IReadOnlyList<Certificate>> GetByCourseIdAsync(
            int courseId, CancellationToken ct = default)
        {
            return await _context.Certificates
                .Where(c => c.CourseId == courseId)
                .Include(c => c.Student)
                .Include(c => c.Course)
                .OrderByDescending(c => c.IssuedAt)
                .AsNoTracking()
                .ToListAsync(ct);
        }

        
        public async Task<bool> ExistsAsync(
            Guid studentId, int courseId, CancellationToken ct = default)
        {
            return await _context.Certificates
                .AnyAsync(c => c.StudentId == studentId &&
                               c.CourseId == courseId, ct);
        }

       
    }
}