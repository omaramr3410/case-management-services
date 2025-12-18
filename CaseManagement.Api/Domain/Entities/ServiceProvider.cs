namespace CaseManagement.Api.Domain.Entities
{
    /// <summary>
    /// DB record definition for ServiceProvider entity
    /// </summary>
    public record ServiceProvider
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;
        public string Region { get; set; } = null!;
        public string ServiceType { get; set; } = null!;

        public DateTime CreatedAt { get; set; }
    }
}