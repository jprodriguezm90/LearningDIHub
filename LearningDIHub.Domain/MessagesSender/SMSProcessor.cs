using LearningDIHub.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningDIHub.Domain.Contracts
{
    public class SMSProcessor : ISenderProvider
    {
        public int Id { get; } = 2;
        public string SenderProcessor(Message msg)
        {
            return $"The Message from {msg.From} to {msg.To} contains {msg.Body} was send by SMS";
        }
    }
}
