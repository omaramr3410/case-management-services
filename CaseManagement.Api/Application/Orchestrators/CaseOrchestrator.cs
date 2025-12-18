using CaseManagement.Api.Application.Orchestrators;
using CaseManagement.Api.Domain.DTOs.Cases;
using CaseManagement.Api.Domain.Entities;
using CaseManagement.Api.DTOs.Cases;
using CaseManagement.Api.Infrastructure.Auditing;
using CaseManagement.Api.Infrastructure.Repositories;
using CaseManagement.Api.Infrastructure.Security;
using Mapster;

namespace CaseManagement.Api.Orchestrators;

public sealed class CaseOrchestrator : ICaseOrchestrator
{
    private readonly ICaseRepository _repo;
    private readonly IAuditService _audit;

    public CaseOrchestrator(ICaseRepository repo, IAuditService audit)
    {
        _repo = repo;
        _audit = audit;
    }

    public async Task<CaseDto> GetByIdAsync(Guid id)
    {
        var entity = await _repo.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("Case not found");

        return entity.Adapt<CaseDto>();
    }

    public async Task<IReadOnlyList<CaseDto>> SearchAsync(CaseQueryRequest filter)
    {
        var cases = await _repo.SearchAsync(filter);
        return cases.Adapt<IReadOnlyList<CaseDto>>();
    }

    public async Task<Guid> CreateAsync(CreateCaseRequest req, UserContext user)
    {
        var entity = req.Adapt<Case>();
        entity.Id = Guid.NewGuid();
        entity.CreatedAt = DateTime.UtcNow;

        var id = await _repo.CreateAsync(entity);

        return id;
    }

    public async Task UpdateAsync(Guid id, UpdateCaseRequest req, UserContext user)
    {
        var entity = await _repo.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("Case not found");

        req.Adapt(entity);
        entity.UpdatedAt = DateTime.UtcNow;

        await _repo.UpdateAsync(entity);
    }
}
