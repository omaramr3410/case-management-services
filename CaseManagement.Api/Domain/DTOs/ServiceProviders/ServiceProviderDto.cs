namespace CaseManagement.Api.Domain.DTOs.ServiceProviders
{
    public record ServiceProviderDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = null!;
        public string Region { get; init; } = null!;
        public string ServiceType { get; init; } = null!;
        public DateTime CreatedAt { get; init; }
    }
}
