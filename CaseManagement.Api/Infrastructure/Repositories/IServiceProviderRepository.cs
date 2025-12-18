using ServiceProvider = CaseManagement.Api.Domain.Entities.ServiceProvider;

namespace CaseManagement.Api.Infrastructure.Repositories
{
    /// <summary>
    /// Interface for Service Provider
    /// </summary>
    public interface IServiceProviderRepository
    {
        Task<ServiceProvider?> GetByIdAsync(Guid id);
        Task<IEnumerable<ServiceProvider>> SearchAsync(string? name, string? region, string? serviceType);
        Task CreateAsync(ServiceProvider provider);
    }
}
