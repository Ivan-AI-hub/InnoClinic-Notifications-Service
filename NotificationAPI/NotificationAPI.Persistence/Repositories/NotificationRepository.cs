using Microsoft.EntityFrameworkCore;
using NotificationAPI.Domain;
using NotificationAPI.Domain.Exceptions;
using NotificationAPI.Domain.Interfaces;

namespace NotificationAPI.Persistence.Repositories
{
    public class NotificationRepository : IScheduledNotificationRepository
    {
        private readonly NotificationContext _context;

        public NotificationRepository(NotificationContext context)
        {
            _context = context;
        }

        public Task CreateAsync(ScheduledNotification scheduledNotification, CancellationToken cancellationToken = default)
        {
            _context.Notifications.Add(scheduledNotification);
            return _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var notification = await _context.Notifications.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (notification == null)
            {
                throw new NotificationNotFoundException(id);
            }
            _context.Notifications.Remove(notification);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public IQueryable<ScheduledNotification> GetByDate(DateTime date, CancellationToken cancellationToken = default)
        {
            return _context.Notifications.Where(x => x.SendAt <= date);
        }
    }
}
