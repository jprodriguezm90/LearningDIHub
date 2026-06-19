using LearningDIHub.Domain.Models;
using Microsoft.Extensions.Logging;

namespace LearningDIHub.MessageSender
{
    [DoNotRegister]
    public sealed class MessageServiceLoggingDecorator(ILogger<MessageService> logger, IMessageService inner) : IMessageService
    {
        public Guid Id { get; } = inner.Id;

        public void Dispose()
        {
            logger.LogCritical("Entering dispose!");
            inner.Dispose();
            logger.LogCritical("Leaving dispose!");
        }

        public string SendMessage(Message msg)
        {
            logger.LogCritical("Entering SendMessage!");
            var result = inner.SendMessage(msg);
            logger.LogCritical("Leaving SendMessage!");
            return result;
        }
    }
}
