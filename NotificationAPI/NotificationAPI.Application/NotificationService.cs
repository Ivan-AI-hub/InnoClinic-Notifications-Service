using NotificationAPI.Application.Abstraction;
using NotificationAPI.Domain.Interfaces;

namespace NotificationAPI.Application
{
    public class NotificationService : INotificationService
    {
        private IScheduledNotificationRepository _notificationRepository;
        private IEmailService _emailService;

        public NotificationService(IScheduledNotificationRepository notificationRepository, IEmailService emailService)
        {
            _notificationRepository = notificationRepository;
            _emailService = emailService;
        }

        public async Task SendAllNotifications(CancellationToken cancellationToken)
        {
            var notifications = _notificationRepository.GetByDate(DateTime.UtcNow).ToList();
            foreach (var notification in notifications)
            {
                await _emailService.SendAsync(notification.SendToEmail, notification.Subject, notification.Message, cancellationToken);
                await _notificationRepository.DeleteAsync(notification.Id, cancellationToken);
            }
        }
    }
}
