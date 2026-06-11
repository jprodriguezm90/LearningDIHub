using LearningDIHub.Domain.Contracts;
using LearningDIHub.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningDIHub.Domain.Services
{
    public class EmailProcessor : ISenderProvider
    {
        public int Id { get; } = 1;
        public string SenderProcessor(Message msg)
        {
            return $"The Message from {msg.From} to {msg.To} contains {msg.Body} was send by Email";
        }
    
    }
}
