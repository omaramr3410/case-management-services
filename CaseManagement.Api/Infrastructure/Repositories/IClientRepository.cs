using CaseManagement.Api.Domain.Entities;
using CaseManagement.Api.Domain.DTOs.Clients;

namespace CaseManagement.Api.Infrastructure.Repositories;

public interface IClientRepository
{
    Task<Client?> GetByIdAsync(int id);
    Task<IEnumerable<Client>> SearchAsync(ClientQueryRequest filter);
    Task<Guid> CreateAsync(Client client);
    Task UpdateAsync(Client client);
    Task SaveChangesAsync();
}
