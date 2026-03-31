using E_Learning.Core.Entities.LiveSessions;

namespace E_Learning.Core.Interfaces.Repositories.LiveSessions
{
    public interface ILiveSessionRepository:IGenericRepository<LiveSession,int>
    {
       IQueryable<LiveSession> GetTableNoTracking() => QueryNoTracking();
        void SoftDelete(LiveSession liveSession);


    }
}