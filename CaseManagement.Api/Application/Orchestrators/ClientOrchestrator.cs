using CaseManagement.Api.Domain.DTOs.Clients;
using CaseManagement.Api.Domain.Entities;
using CaseManagement.Api.DTOs.Clients;
using CaseManagement.Api.Infrastructure.Auditing;
using CaseManagement.Api.Infrastructure.Repositories;
using CaseManagement.Api.Infrastructure.Security;
using Mapster;

namespace CaseManagement.Api.Application.Orchestrators;

public sealed class ClientOrchestrator : IClientOrchestrator
{
    private readonly IClientRepository clientRepository;
    private readonly IAuditService _audit;

    public ClientOrchestrator(IClientRepository clientRepository, IAuditService audit)
    {
        this.clientRepository = clientRepository;
        _audit = audit;
    }

    public async Task<ClientDto> GetByIdAsync(int id)
    {
        var client = await clientRepository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("Client not found");

        return client.Adapt<ClientDto>();
    }

    public async Task<IReadOnlyList<ClientDto>> SearchAsync(ClientQueryRequest filter)
    {
        var clients = await clientRepository.SearchAsync(filter);

        return clients.Adapt<IReadOnlyList<ClientDto>>();
    }

    public async Task<Guid> CreateAsync(CreateClientRequest req, UserContext user)
    {
        var client = req.Adapt<Client>();
        client.CreatedAt = DateTime.UtcNow;

        var id = await clientRepository.CreateAsync(client);

        await _audit.LogAsync(user, "Client", id.ToString(), "CREATE");

        return id;
    }

    public async Task UpdateAsync(int id, UpdateClientRequest req, UserContext user)
    {
        var client = await clientRepository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("Client not found");

        req.Adapt(client);

        await clientRepository.UpdateAsync(client);

        await _audit.LogAsync(user, "Client", id.ToString(), "UPDATE");
    }
}
