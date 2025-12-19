using CaseManagement.Api.Domain.DTOs.ServiceProviders;
using CaseManagement.Api.Infrastructure.Auditing;
using CaseManagement.Api.Infrastructure.Repositories;
using CaseManagement.Api.Infrastructure.Security;
using Mapster;
using ServiceProvider = CaseManagement.Api.Domain.Entities.ServiceProvider;

namespace CaseManagement.Api.Application.Orchestrators
{
    public class ServiceProviderOrchestrator : IServiceProviderOrchestrator
    {
        private readonly IServiceProviderRepository _repo;
        private readonly IAuditService _audit;

        public ServiceProviderOrchestrator(IServiceProviderRepository repo, IAuditService audit)
        {
            _repo = repo;
            _audit = audit;
        }

        public async Task<ServiceProviderDto> CreateAsync(CreateServiceProviderRequest request, UserContext user)
        {
            var entity = request.Adapt<ServiceProvider>();
            entity.Id = Guid.NewGuid();
            entity.CreatedAt = DateTime.UtcNow;

            await _repo.CreateAsync(entity);

            //await _audit.LogAsync(user, "ServiceProvider", entity.Id.ToString(), "CREATE");

            return entity.Adapt<ServiceProviderDto>();
        }

        public async Task<ServiceProviderDto?> GetByIdAsync(Guid id)
        {
            var entity = await _repo.GetByIdAsync(id);

            return entity?.Adapt<ServiceProviderDto>();
        }

        public async Task<IEnumerable<ServiceProviderDto>> SearchAsync(ServiceProviderQueryRequest request)
        {
            var results = await _repo.SearchAsync(request.Name, request.Region, request.ServiceType);

            return results.Adapt<IEnumerable<ServiceProviderDto>>();
        }
    }
}
