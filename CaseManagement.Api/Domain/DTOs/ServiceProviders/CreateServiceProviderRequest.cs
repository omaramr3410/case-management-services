namespace CaseManagement.Api.Domain.DTOs.ServiceProviders
{
    public record CrreateServiceProviderRequest
    {
        public string Name { get; init; } = null!;
        public string Region { get; init; } = null!;
        public string ServiceType { get; init; } = null!;
    }
}
