using CaseManagement.Api.Domain.DTOs.Cases;
using CaseManagement.Api.DTOs.Cases;
using CaseManagement.Api.Infrastructure.Security;

namespace CaseManagement.Api.Application.Orchestrators;

public interface ICaseOrchestrator
{
    Task<CaseDto> GetByIdAsync(Guid id);
    Task<IReadOnlyList<CaseDto>> SearchAsync(CaseQueryRequest filter);
    Task<Guid> CreateAsync(CreateCaseRequest request, UserContext user);
    Task UpdateAsync(Guid id, UpdateCaseRequest request, UserContext user);
}
