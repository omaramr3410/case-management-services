namespace CaseManagement.Api.Domain.Entities
{
    /// <summary>
    /// DB record definition for Case entity
    /// </summary>
    public record Case
    {
        public Guid Id { get; set; }

        public Guid ClientId { get; set; }
        public Client Client { get; set; } = null!;

        public Guid? AssignedOfficerId { get; set; }
        public Officer? AssignedOfficer { get; set; }

        public Guid? ServiceProviderId { get; set; }
        public ServiceProvider? ServiceProvider { get; set; }

        public string Status { get; set; } = null!;
        public string Region { get; set; } = null!;
        public string? Recommendations { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}