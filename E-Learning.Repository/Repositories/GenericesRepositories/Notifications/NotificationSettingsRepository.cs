using E_Learning.Core.Interfaces.Repositories.Notifications;
using E_Learning.Repository.Data;

namespace E_Learning.Repository.Repositories.GenericesRepositories.Notifications
{
    public class NotificationSettingsRepository : INotificationSettingsRepository
    {
        public NotificationSettingsRepository(ELearningDbContext context)
        {
            _context = context;
        }

        public ELearningDbContext _context { get; }
    }
}
