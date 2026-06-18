using LearningDIHub.Domain.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace LearningDIHub.MessageSender
{
    [AsSingletonAtribute]
    public class MessageService(IOptions<ServiceSelector> serviceSelector, IEnumerable<ISenderProvider> senderProvider) : IMessageService , IDisposable
    {

        public Guid Id { get; } = Guid.NewGuid();

        public string SendMessage(Message msg)
        {
            var result = string.Empty;
            var count = 0;
            foreach (var provider in senderProvider)
            {
                if (IsServiceEnabled(provider))
                    result += $"Mensaje {count++}: {provider.SenderProcessor(msg)}\n";
            }
            return result;
        }

        public bool IsServiceEnabled(ISenderProvider sender)
        {
            return sender.Id.Equals(serviceSelector.Value.SelectedService);
           
        }

        public void Dispose()
        {
            Console.WriteLine($"MessageService Is Disposed {Id}");
        }
    }
}
