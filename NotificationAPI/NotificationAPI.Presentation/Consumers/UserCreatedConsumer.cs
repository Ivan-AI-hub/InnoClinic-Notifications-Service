using MassTransit;
using NotificationAPI.Application.Abstraction;
using NotificationAPI.Application.Abstraction.Models;
using SharedEvents.Models;

namespace NotificationAPI.Presentation.Consumers
{
    public class UserCreatedConsumer : IConsumer<UserCreated>
    {
        private readonly IEmailSendingService _emailSendingService;
        private readonly IUserService _userService;

        public UserCreatedConsumer(IEmailSendingService emailSendingService, IUserService userService)
        {
            _emailSendingService = emailSendingService;
            _userService = userService;
        }

        public async Task Consume(ConsumeContext<UserCreated> context)
        {
            var message = context.Message;
            await _userService.AddAsync(new UserDTO(message.Id, message.UserEmail));
            await _emailSendingService.SendVerifyEmailMessage(message.UserEmail, message.ConfirmEmailUrl);
        }
    }
}
