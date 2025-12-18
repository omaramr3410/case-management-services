using CaseManagement.Api.Domain.DTOs.Officers;
using CaseManagement.Api.Domain.Entities;
using CaseManagement.Api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CaseManagement.Api.Infrastructure.Repositories;

public sealed class OfficerRepository : IOfficerRepository
{
    private readonly AppDbContext _db;

    public OfficerRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Officer?> GetByIdAsync(Guid id)
    {
        return await _db.Officer.FindAsync(id);
    }

    public async Task<IReadOnlyList<Officer>> SearchAsync(OfficerQueryRequest f)
    {
        return await _db.Officer
            .Where(o =>
                (f.FirstName == null || o.FirstName.Contains(f.FirstName)) &&
                (f.LastName == null || o.LastName.Contains(f.LastName)) &&
                (f.Region == null || o.Region == f.Region)
            )
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Guid> CreateAsync(Officer entity)
    {
        _db.Officer.Add(entity);

        await _db.SaveChangesAsync();

        return entity.Id;
    }

    public async Task UpdateAsync(Officer entity)
    {
        _db.Officer.Update(entity);

        await _db.SaveChangesAsync();
    }
}
