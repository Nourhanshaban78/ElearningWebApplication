using E_Learning.Core.Interfaces.Repositories.LiveSessions;
using E_Learning.Repository.Data;

namespace E_Learning.Repository.Repositories.GenericesRepositories.LiveSessions
{
    public class LiveSessionAttendeeRepository : ILiveSessionAttendeeRepository
    {
        public LiveSessionAttendeeRepository(ELearningDbContext context)
        {
            _context = context;
        }

        public ELearningDbContext _context { get; }
    }
}
