using E_Learning.Core.Entities.LiveSessions;
using E_Learning.Core.Entities.Profiles;

namespace E_Learning.Core.Interfaces.Repositories.LiveSessions
{
  // أضفنا الوراثة من IGenericRepository
  public interface ILiveSessionAttendeeRepository : IGenericRepository<LiveSessionAttendee, int>
  {
    Task<bool> IsStudentCurrentlyInSessionAsync(int sessionId, Guid studentId, CancellationToken ct = default); Task<IReadOnlyList<LiveSessionAttendee>> GetAttendeesBySessionIdAsync(int sessionId, CancellationToken ct = default);
    Task<LiveSessionAttendee?> GetFullActiveAttendeeAsync(int sessionId, Guid studentId, CancellationToken ct = default);
    IQueryable<StudentProfile> GetTableNoTracking();
    void LeaveSession(LiveSessionAttendee liveSessionAttendee);
  }
}