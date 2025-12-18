using CaseManagement.Api.Domain.DTOs.Officers;
using CaseManagement.Api.Infrastructure.Security;

namespace CaseManagement.Api.Application.Orchestrators;

public interface IOfficerOrchestrator
{
    Task<OfficerDto> GetByIdAsync(Guid id);
    Task<IReadOnlyList<OfficerDto>> SearchAsync(OfficerQueryRequest filter);
    Task<Guid> CreateAsync(OfficerCreateRequest request, UserContext user);
    Task UpdateAsync(Guid id, UpdateOfficerRequest request, UserContext user);
}
