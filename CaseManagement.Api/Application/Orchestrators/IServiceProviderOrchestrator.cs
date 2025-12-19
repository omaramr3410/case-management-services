using CaseManagement.Api.Domain.DTOs.ServiceProviders;
using CaseManagement.Api.Infrastructure.Security;

namespace CaseManagement.Api.Application.Orchestrators
{
    public interface IServiceProviderOrchestrator
    {
        Task<ServiceProviderDto?> GetByIdAsync(Guid id);
        Task<IEnumerable<ServiceProviderDto>> SearchAsync(ServiceProviderQueryRequest request);
        Task<ServiceProviderDto> CreateAsync(CreateServiceProviderRequest request, UserContext user);
    }
}
