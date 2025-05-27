using Microsoft.EntityFrameworkCore;
using Server.Interfaces;
using Shared.Entities;
using System;

namespace Server.Repositories
{
    public class NotificationService : INotificationService
    {
        private readonly HospitalDbContext _context;

        public NotificationService(HospitalDbContext context)
        {
            _context = context;
        }

        public async Task AddNotificationAsync(string message)
        {
            var notification = new Notification
            {
                Message = message,
                Timestamp = DateTime.UtcNow
            };

            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Notification>> GetRecentNotificationsAsync(int count = 10)
        {
            return await _context.Notifications
                .OrderByDescending(n => n.Timestamp)
                .Take(count)
                .ToListAsync();
        }
    }
}
