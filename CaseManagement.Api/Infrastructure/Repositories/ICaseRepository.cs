using CaseManagement.Api.Domain.Entities;
using CaseManagement.Api.DTOs.Cases;

namespace CaseManagement.Api.Infrastructure.Repositories;

public interface ICaseRepository
{
    Task<Case?> GetByIdAsync(Guid id);
    Task<IReadOnlyList<Case>> SearchAsync(CaseQueryRequest f);
    Task<Guid> CreateAsync(Case entity);
    Task UpdateAsync(Case entity);
    Task SaveChangesAsync();
}
