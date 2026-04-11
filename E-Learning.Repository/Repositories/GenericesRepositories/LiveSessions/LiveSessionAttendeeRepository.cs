using E_Learning.Core.Entities.LiveSessions;
using E_Learning.Core.Entities.Profiles;
using E_Learning.Core.Interfaces.Repositories.LiveSessions;
using E_Learning.Repository.Data;
using Microsoft.EntityFrameworkCore;

namespace E_Learning.Repository.Repositories.GenericesRepositories.LiveSessions
{
    public class LiveSessionAttendeeRepository : GenericRepository<LiveSessionAttendee, int>, ILiveSessionAttendeeRepository
    {
        public LiveSessionAttendeeRepository(ELearningDbContext context) : base(context) { }

        // ميثود مركزية تجلب شجرة البيانات كاملة (Attendee -> Session -> Course & Instructor)
        private IQueryable<LiveSessionAttendee> WithFullIncludes()
        {
            return _context.LiveSessionAttendees
                .Include(a => a.Student)            // بيانات الطالب
                .Include(a => a.Session)            // الجلسة
                    .ThenInclude(s => s.Course)     // الكورس داخل الجلسة
                .Include(a => a.Session)
                    .ThenInclude(s => s.Instructor) // المدرس داخل الجلسة
                .AsNoTracking();
        }
        // تعديل الميثود لتفحص فقط الطلاب "الموجودين حالياً"
        public async Task<bool> IsStudentCurrentlyInSessionAsync(int sessionId, Guid studentId, CancellationToken ct = default)
        {
            return await _context.LiveSessionAttendees
                         .AnyAsync(x => x.SessionId == sessionId &&
                                        x.StudentId == studentId &&
                                        x.LeftAt == null, ct);
        }
        public async Task<IReadOnlyList<LiveSessionAttendee>> GetAttendeesBySessionIdAsync(int sessionId, CancellationToken ct = default)
        {
            return await WithFullIncludes()
                         .Where(x => x.SessionId == sessionId)
                         .ToListAsync(ct);
        }

        public async Task<LiveSessionAttendee?> GetFullActiveAttendeeAsync(int sessionId, Guid studentId, CancellationToken ct = default)
        {
            return await WithFullIncludes()
                         .FirstOrDefaultAsync(x => x.SessionId == sessionId && x.StudentId == studentId && x.LeftAt == null, ct);
        }

        public async Task<bool> IsStudentEnrolledAsync(int sessionId, Guid studentId, CancellationToken ct = default)
        {
            return await _context.LiveSessionAttendees
                         .AnyAsync(x => x.SessionId == sessionId && x.StudentId == studentId, ct);
        }

        public IQueryable<StudentProfile> GetTableNoTracking()
        {
            return _context.StudentProfiles.AsNoTracking();
        }

        public void LeaveSession(LiveSessionAttendee attendee)
        {
            attendee.LeftAt = DateTime.UtcNow;
            attendee.DurationSeconds = (int)(DateTime.UtcNow - attendee.JoinedAt).TotalSeconds;
            _context.Set<LiveSessionAttendee>().Update(attendee);
        }
    }
}