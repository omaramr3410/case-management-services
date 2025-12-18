using CaseManagement.Api.Domain.Entities;
using CaseManagement.Api.DTOs.Cases;
using CaseManagement.Api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CaseManagement.Api.Infrastructure.Repositories;

public class CaseRepository : ICaseRepository
{
    private readonly AppDbContext _db;

    public CaseRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Case?> GetByIdAsync(Guid id)
    {
        return await _db.Case.FindAsync(id);
    }

    public async Task<IReadOnlyList<Case>> SearchAsync(CaseQueryRequest f)
    {
        return await _db.Case
            .Where(c =>
                (f.ClientId == null || c.ClientId == f.ClientId) &&
                (f.AssignedOfficerId == null || c.AssignedOfficerId == f.AssignedOfficerId) &&
                (f.ServiceProviderId == null || c.ServiceProviderId == f.ServiceProviderId) &&
                (f.Status == null || c.Status == f.Status) &&
                (f.Region == null || c.Region == f.Region) &&
                (f.CreatedAt == null || c.CreatedAt.Date == f.CreatedAt.Value.Date) &&
                (f.UpdatedAt == null || c.UpdatedAt!.Value.Date == f.UpdatedAt.Value.Date)
            )
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Guid> CreateAsync(Case entity)
    {
        _db.Case.Add(entity);

        await _db.SaveChangesAsync();

        return entity.Id;
    }

    public async Task UpdateAsync(Case entity)
    {
        _db.Case.Update(entity);
        await _db.SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
        => await _db.SaveChangesAsync();
}
