using E_Learning.Core.Interfaces.Repositories.Schedule;
using E_Learning.Repository.Data;

namespace E_Learning.Repository.Repositories.GenericesRepositories.Schedule
{
    public class ScheduleEventRepository : IScheduleEventRepository
    {
        public ScheduleEventRepository(ELearningDbContext context)
        {
            _context = context;
        }

        public ELearningDbContext _context { get; }
    }
}
