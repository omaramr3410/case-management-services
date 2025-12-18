namespace CaseManagement.Api.Entities
{
    /// <summary>
    /// AuditLog record definition 
    /// </summary>
    public record AuditLog
    {
        public int Id { get; set; }

        // Actor
        public Guid? UserId { get; set; }
        public string? Username { get; set; }
        public string? UserRole { get; set; }

        // Action
        public string EntityName { get; set; } = null!;
        public string EntityId { get; set; } = null!;
        public string Action { get; set; } = null!;

        // Timing
        public DateTime TimestampUtc { get; set; }

        // Context
        public string? IpAddress { get; set; }

        // JSON metadata (optional)
        public string? Metadata { get; set; }
    }
}