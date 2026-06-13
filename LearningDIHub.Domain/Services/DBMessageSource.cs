using LearningDIHub.Domain.Contracts;
using LearningDIHub.Domain.Models;

namespace LearningDIHub.Domain.Services
{
    public class DBMessageSource() : IMessageSource
    {
        public Message GetMessage()
        {
            var msg = new Message() { Id = Guid.NewGuid(), From = "Juan", To = "Dani", Body = "I love you from the bottom of my DB." };
            return msg;
        }
    }
}
