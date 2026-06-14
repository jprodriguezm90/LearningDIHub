namespace LearningDIHub.Domain.Auditing
{
    public interface IAuditLogger<TObject>
    {
        void AuditUpdate(Principal principal, TObject updatedObject);
        void AuditUpdate(Principal principal, TObject updatedObject, string action);
    }
}
