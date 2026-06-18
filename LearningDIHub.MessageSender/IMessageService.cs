using LearningDIHub.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningDIHub.MessageSender
{
    public interface IMessageService
    {
        public Guid Id { get; }
        public string SendMessage(Message msg);
    }
}
