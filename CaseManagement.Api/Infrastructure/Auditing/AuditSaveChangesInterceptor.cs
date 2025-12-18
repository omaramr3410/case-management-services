using CaseManagement.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Text.Json;
using CaseManagement.Api.Infrastructure.Security;

namespace CaseManagement.Api.Infrastructure.Auditing;

public class AuditSaveChangesInterceptor : SaveChangesInterceptor
{
    private readonly IUserContext _userContext;

    public AuditSaveChangesInterceptor(IUserContext userContext)
    {
        _userContext = userContext;
    }

    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData,
        InterceptionResult<int> result)
    {
        AddAuditLogs(eventData.Context!);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        AddAuditLogs(eventData.Context!);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void AddAuditLogs(DbContext context)
    {
        var entries = context.ChangeTracker.Entries()
            .Where(e =>
                e.State == EntityState.Added ||
                e.State == EntityState.Modified ||
                e.State == EntityState.Deleted)
            .Where(e => e.Entity is not AuditLog)
            .ToList(); // 👈 critical fix

        foreach (var entry in entries)
        {
            var audit = new AuditLog
            {
                UserId = _userContext.UserId,
                Username = _userContext.Username,
                UserRole = _userContext.UserRole,

                EntityName = entry.Entity.GetType().Name,
                EntityId = GetPrimaryKey(entry),

                Action = entry.State.ToString(),
                TimestampUtc = DateTime.UtcNow,
                IpAddress = _userContext.IpAddress,

                Metadata = JsonSerializer.Serialize(new
                {
                    entry.State,
                    Values = entry.CurrentValues.ToObject()
                })
            };

            context.Set<AuditLog>().Add(audit);
        }
    }


    private static string GetPrimaryKey(Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry entry)
    {
        var key = entry.Metadata.FindPrimaryKey();
        if (key == null) return "UNKNOWN";

        return string.Join(",",
            key.Properties.Select(p => entry.Property(p.Name).CurrentValue?.ToString()));
    }
}
