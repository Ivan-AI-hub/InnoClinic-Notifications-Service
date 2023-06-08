using MassTransit;
using NotificationAPI.Application.Abstraction;
using SharedEvents.Models;

namespace NotificationAPI.Presentation.Consumers
{
    public class UserCreatedConsumer : IConsumer<UserCreated>
    {
        private readonly IEmailSendingService _emailSendingService;

        public UserCreatedConsumer(IEmailSendingService emailSendingService)
        {
            _emailSendingService = emailSendingService;
        }

        public async Task Consume(ConsumeContext<UserCreated> context)
        {
            var message = context.Message;
            await _emailSendingService.SendVerifyEmailMessage(message.Email, message.ConfirmEmailUrl);
        }
    }
}
