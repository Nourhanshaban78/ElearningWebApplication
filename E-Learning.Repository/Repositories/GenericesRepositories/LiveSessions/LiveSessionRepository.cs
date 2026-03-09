using E_Learning.Core.Interfaces.Repositories.LiveSessions;
using E_Learning.Repository.Data;

namespace E_Learning.Repository.Repositories.GenericesRepositories.LiveSessions
{
    public class LiveSessionRepository  : ILiveSessionRepository
    {
        public LiveSessionRepository(ELearningDbContext context)
        {
            _context = context;
        }

        public ELearningDbContext _context { get; }
    }
}
