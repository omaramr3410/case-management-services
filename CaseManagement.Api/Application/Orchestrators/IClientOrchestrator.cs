using CaseManagement.Api.Domain.DTOs.Clients;
using CaseManagement.Api.DTOs.Clients;
using CaseManagement.Api.Entities;
using CaseManagement.Api.Infrastructure.Security;

namespace CaseManagement.Api.Application.Orchestrators;

public interface IClientOrchestrator
{
    Task<ClientDto> GetByIdAsync(int id);

    Task<IReadOnlyList<ClientDto>> SearchAsync(ClientQueryRequest filter);

    Task<Guid> CreateAsync(CreateClientRequest request, UserContext user);

    Task UpdateAsync(int id, UpdateClientRequest req, UserContext user);
}
