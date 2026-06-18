using LearningDIHub.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningDIHub.MessageSender
{
    public interface ISenderProvider
    {
        public int Id { get; }
        public string SenderProcessor(Message msg);
    }
}
