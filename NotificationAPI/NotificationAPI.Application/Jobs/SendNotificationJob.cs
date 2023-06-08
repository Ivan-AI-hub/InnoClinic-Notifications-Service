using NotificationAPI.Application.Abstraction;
using NotificationAPI.Domain.Interfaces;
using Quartz;

namespace NotificationAPI.Application.Jobs
{
    public class SendNotificationJob : IJob
    {
        private IScheduledNotificationRepository _notificationRepository;
        private IEmailService _emailService;

        public SendNotificationJob(IScheduledNotificationRepository notificationRepository, IEmailService emailService)
        {
            _notificationRepository = notificationRepository;
            _emailService = emailService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var notifications = _notificationRepository.GetByDate(DateTime.UtcNow);
            foreach (var notification in notifications)
            {
                await _emailService.SendAsync(notification.SendToEmail, notification.Subject, notification.Message, context.CancellationToken);
                await _notificationRepository.DeleteAsync(notification.Id, context.CancellationToken);
            }
        }
    }
}
