using CaseManagement.Api.Domain.DTOs.Clients;
using CaseManagement.Api.Domain.Entities;
using CaseManagement.Api.Infrastructure.Data;
using CaseManagement.Api.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CaseManagement.Api.Repositories;

public sealed class ClientRepository : IClientRepository
{
    private readonly AppDbContext _db;

    public ClientRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Client?> GetByIdAsync(int id)
    {
        return await _db.Client.FindAsync(id);
    }

    public async Task<IEnumerable<Client>> SearchAsync(ClientQueryRequest f)
    {
        return await _db.Client
            .Where(c =>
                (f.FirstName == null || c.FirstName.Contains(f.FirstName)) &&
                (f.LastName == null || c.LastName.Contains(f.LastName)) &&
                (f.Region == null || c.Region == f.Region) &&
                (f.Status == null || c.Status == f.Status) &&
                (f.SSN == null || c.SSN == f.SSN) &&
                (f.DateOfBirth == null || c.DateOfBirth == f.DateOfBirth)
            )
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Guid> CreateAsync(Client client)
    {
        _db.Client.Add(client);

        await _db.SaveChangesAsync();

        return client.Id;
    }

    public async Task UpdateAsync(Client client)
    {
        _db.Client.Update(client);

        await _db.SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
        => await _db.SaveChangesAsync();
}
