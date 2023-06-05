using MassTransit;
using Microsoft.Extensions.Logging;

namespace NotificationAPI.Presentation.Consumers
{
    public class ConsumeObserver : IConsumeObserver
    {
        private ILogger<ConsumeObserver> _logger;

        public ConsumeObserver(ILogger<ConsumeObserver> logger)
        {
            _logger = logger;
        }

        public Task ConsumeFault<T>(ConsumeContext<T> context, Exception exception) where T : class
        {
            _logger.LogError(exception, "Exception in Consumer occur");
            return Task.CompletedTask;
        }

        public Task PostConsume<T>(ConsumeContext<T> context) where T : class
        {
            return Task.CompletedTask;
        }

        public Task PreConsume<T>(ConsumeContext<T> context) where T : class
        {
            return Task.CompletedTask;
        }
    }
}
