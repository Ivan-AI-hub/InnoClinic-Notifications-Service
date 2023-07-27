namespace NotificationAPI.Application.Abstraction
{
    public interface INotificationService
    {
        public Task SendAllNotifications(CancellationToken cancellationToken);
    }
}
