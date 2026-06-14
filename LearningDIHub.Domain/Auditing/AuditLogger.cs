using System.Text.Json;

namespace LearningDIHub.Domain.Auditing
{
    public sealed class AuditLogger<TObject> : IAuditLogger<TObject>
    {
        public void AuditUpdate(Principal principal, TObject updatedObject)
        {
            System.Console.WriteLine($"Principal '{principal.DisplayName}' made the following changes: {JsonSerializer.Serialize(updatedObject)}");
        }
        public void AuditUpdate(Principal principal, TObject updatedObject, string action = "changes")
        {
            System.Console.WriteLine($"Principal '{principal.DisplayName}' made the following {action}: {JsonSerializer.Serialize(updatedObject)}");
        }
    }
}
