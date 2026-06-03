using LearningDIHub.Domain.Contracts;
using LearningDIHub.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningDIHub.Domain.Services
{
    public class MessageService : IMessageService
    {
        private readonly IEnumerable<ISenderProvider> _senderProvider;

        public Guid Id { get; } = Guid.NewGuid();
        public MessageService(IEnumerable<ISenderProvider> sender)
        {
            _senderProvider = sender;
        }
        public string SendMessage(Message msg)
        {
            var result = string.Empty;
            var count = 0;
            foreach (var provider in _senderProvider)
            {
                result += $"Mensaje {count++}: {provider.SenderProcessor(msg)}\n";
            }
            return result;
        }
    }
}
