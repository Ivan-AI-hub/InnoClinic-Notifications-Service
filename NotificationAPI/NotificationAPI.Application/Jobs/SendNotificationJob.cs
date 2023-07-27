using NotificationAPI.Application.Abstraction;
using NotificationAPI.Domain.Interfaces;
using Quartz;

namespace NotificationAPI.Application.Jobs
{
    public class SendNotificationJob : IJob
    {
        private INotificationService _notificationService;

        public SendNotificationJob(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await _notificationService.SendAllNotifications(context.CancellationToken);
        }
    }
}
