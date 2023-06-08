namespace NotificationAPI.Domain.Interfaces
{
    public interface IScheduledNotificationRepository
    {
        public Task CreateAsync(ScheduledNotification scheduledNotification, CancellationToken cancellationToken = default);
        public Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        public IQueryable<ScheduledNotification> GetByDate(DateTime date, CancellationToken cancellationToken = default);
    }
}
