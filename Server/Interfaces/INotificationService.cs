using Shared.Entities;

namespace Server.Interfaces
{
    public interface INotificationService
    {
        Task AddNotificationAsync(string message);
        Task<List<Notification>> GetRecentNotificationsAsync(int count = 10);
    }
}