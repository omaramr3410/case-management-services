using CaseManagement.Api.Domain.DTOs.Officers;
using CaseManagement.Api.Domain.Entities;

namespace CaseManagement.Api.Infrastructure.Repositories;

/// <summary>
/// Repository interface to interact with Officer entities
/// </summary>
public interface IOfficerRepository
{
    Task<Officer?> GetByIdAsync(Guid id);

    Task<IReadOnlyList<Officer>> SearchAsync(OfficerQueryRequest filter);

    Task<Guid> CreateAsync(Officer entity);

    Task UpdateAsync(Officer entity);
}
