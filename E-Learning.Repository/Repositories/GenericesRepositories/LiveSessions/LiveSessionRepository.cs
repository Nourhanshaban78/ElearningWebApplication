using E_Learning.Core.Entities.LiveSessions;
using E_Learning.Core.Interfaces.Repositories.LiveSessions;
using E_Learning.Repository.Data;
using Microsoft.EntityFrameworkCore;

namespace E_Learning.Repository.Repositories.GenericesRepositories.LiveSessions
{
    public class LiveSessionRepository : GenericRepository<LiveSession, int>, ILiveSessionRepository
    {
        public LiveSessionRepository(ELearningDbContext context) : base(context) { }

        // ميثود داخلية تجمع كل الـ Includes المطلوبة للمابينج تبعك
        private IQueryable<LiveSession> WithFullIncludes()
        {
            return _context.LiveSessions
                .Include(ls => ls.Course)           // عشان CourseTitle و CourseSummaryDto
                .Include(ls => ls.Instructor)       // عشان InstructorResponseDto (ApplicationUser)
                .Include(ls => ls.Attendees)        // عشان AttendeeResponseDto
                    .ThenInclude(a => a.Student)    // عشان بيانات الطالب اللي حضر
                .AsNoTracking();
        }

        public override async Task<LiveSession?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            return await WithFullIncludes().FirstOrDefaultAsync(e => e.Id == id, ct);
        }

        public override async Task<IReadOnlyList<LiveSession>> GetAllAsync(CancellationToken ct = default)
        {
            return await WithFullIncludes().ToListAsync(ct);
        }

        public void SoftDelete(LiveSession liveSession)
        {
            _context.Set<LiveSession>().Remove(liveSession);
        }
    }
}