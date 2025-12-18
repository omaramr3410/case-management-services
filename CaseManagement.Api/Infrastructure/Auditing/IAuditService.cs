using CaseManagement.Api.Infrastructure.Security;

namespace CaseManagement.Api.Infrastructure.Auditing
{
    /// <summary>
    /// Defines service interface 
    /// Service exposes lightweight audit logging service
    /// </summary>
    public interface IAuditService
    {
        //Update DB set with new AuditLog entity
        //Needs DB Save call
        Task LogAsync(
            UserContext user,
            string entityName,
            object entityId,
            string action,
            object? metadata = null
        );

        //Update DB set with new AuditLog entity
        //Needs DB Save call
        void Log(
            UserContext user,
            string entityName,
            object entityId,
            string action,
            object? metadata = null
        );
    }
}