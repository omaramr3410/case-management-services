namespace CaseManagement.Api.Domain.DTOs.ServiceProviders
{
    public record ServiceProviderQueryRequest
    {
        public string? Name { get; init; }
        public string? Region { get; init; }
        public string? ServiceType { get; init; }
    }
}
