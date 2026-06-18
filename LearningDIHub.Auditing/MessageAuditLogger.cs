using LearningDIHub.Domain.Models;
using System.Text.Json;

namespace LearningDIHub.Auditing
{
    public sealed class MessageAuditLogger : IAuditLogger<Message>
    {
        public void AuditUpdate(Principal principal, Message updatedObject)
        {
            System.Console.WriteLine($"Principal '{principal.DisplayName}' made the following message changes: {JsonSerializer.Serialize(updatedObject)}");
        }
        public void AuditUpdate(Principal principal, Message updatedObject, string action = "changes")
        {
            System.Console.WriteLine($"Principal '{principal.DisplayName}' made the following message {action}: {JsonSerializer.Serialize(updatedObject)}");
        }
    }
}
