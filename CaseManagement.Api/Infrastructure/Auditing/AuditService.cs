using System.Text.Json;
using CaseManagement.Api.Entities;
using CaseManagement.Api.Infrastructure.Data;
using CaseManagement.Api.Infrastructure.Security;

namespace CaseManagement.Api.Infrastructure.Auditing
{
    /// <summary>
    /// DB Audit service 
    /// Implement logging process for API actions
    /// Caller of service must save changes to DB via async call -  _db.SaveChangesAsync()
    /// </summary>
    public class AuditService : IAuditService
    {
        private readonly AppDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuditService(AppDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
        }

        public Task LogAsync(
            UserContext user,
            string entityName,
            object entityId,
            string action,
            object? metadata = null)
        {
            var ipAddress = _httpContextAccessor.HttpContext?.Request?.
                HttpContext?.Connection.RemoteIpAddress?.ToString();

            _db.AuditLog.Add(new AuditLog
            {
                UserId = user.UserId,
                Username = user.Username,
                UserRole = user.UserRole,
                EntityName = entityName,
                EntityId = entityId.ToString() ?? "----",
                Action = action,
                IpAddress = ipAddress,
                Metadata = metadata != null
                    ? JsonSerializer.Serialize(metadata)
                    : null,
                TimestampUtc = DateTime.UtcNow
            });

            return Task.CompletedTask;
        }
        public void Log(
            UserContext user,
            string entityName,
            object entityId,
            string action,
            object? metadata = null)
        {
            var ipAddress = _httpContextAccessor.HttpContext?.Request?.
                HttpContext?.Connection.RemoteIpAddress?.ToString();

            _db.AuditLog.Add(new AuditLog
            {
                UserId = user.UserId,
                Username = user.Username,
                UserRole = user.UserRole,
                EntityName = entityName,
                EntityId = entityId.ToString() ?? "----",
                Action = action,
                IpAddress = ipAddress,
                Metadata = metadata != null
                    ? JsonSerializer.Serialize(metadata)
                    : null,
                TimestampUtc = DateTime.UtcNow
            });
        }
    }
}