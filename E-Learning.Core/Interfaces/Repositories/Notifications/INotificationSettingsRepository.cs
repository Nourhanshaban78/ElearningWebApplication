using E_Learning.Core.Entities.Notifications;

namespace E_Learning.Core.Interfaces.Repositories.Notifications
{
    public interface INotificationSettingsRepository : IGenericRepository<NotificationSetting, Guid>
    {
        Task<NotificationSetting?> GetByUserIdAsync(Guid userId);
    }
}