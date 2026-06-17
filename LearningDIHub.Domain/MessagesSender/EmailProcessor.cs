using LearningDIHub.Domain.Auditing;
using LearningDIHub.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningDIHub.Domain.Contracts
{
    public class EmailProcessor(IAuditLogger<Message> auditLogger) : ISenderProvider
    {
        public int Id { get; } = 1;
        public Principal _principal = new("EmailProcessor");
        public string SenderProcessor(Message msg)
        {

            auditLogger.AuditUpdate(_principal, msg, "EmailSent");
            return $"The Message from {msg.From} to {msg.To} contains {msg.Body} was sent by Email";
        }
    
    }
}
