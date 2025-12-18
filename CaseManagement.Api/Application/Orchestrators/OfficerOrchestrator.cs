using CaseManagement.Api.Application.Orchestrators;
using CaseManagement.Api.Domain.DTOs.Officers;
using CaseManagement.Api.Domain.Entities;
using CaseManagement.Api.Infrastructure.Auditing;
using CaseManagement.Api.Infrastructure.Repositories;
using CaseManagement.Api.Infrastructure.Security;
using Mapster;

namespace CaseManagement.Api.Orchestrators;

public sealed class OfficerOrchestrator : IOfficerOrchestrator
{
    private readonly IOfficerRepository _repo;
    private readonly IAuditService _audit;

    public OfficerOrchestrator(IOfficerRepository repo, IAuditService audit)
    {
        _repo = repo;
        _audit = audit;
    }

    public async Task<OfficerDto> GetByIdAsync(Guid id)
    {
        var officer = await _repo.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("Officer not found");

        return officer.Adapt<OfficerDto>();
    }

    public async Task<IReadOnlyList<OfficerDto>> SearchAsync(OfficerQueryRequest filter)
    {
        var officers = await _repo.SearchAsync(filter);

        return officers.Adapt<IReadOnlyList<OfficerDto>>();
    }

    public async Task<Guid> CreateAsync(OfficerCreateRequest req, UserContext user)
    {
        var entity = req.Adapt<Officer>();
        entity.Id = Guid.NewGuid();
        entity.CreatedAt = DateTime.UtcNow;

        var id = await _repo.CreateAsync(entity);

        //await _audit.LogAsync(user, "Officer", id.ToString(), "CREATE");

        return id;
    }

    public async Task UpdateAsync(Guid id, UpdateOfficerRequest req, UserContext user)
    {
        var entity = await _repo.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("Officer not found");

        req.Adapt(entity);

        await _repo.UpdateAsync(entity);

        //await _audit.LogAsync(user, "Officer", id.ToString(), "UPDATE");
    }
}
