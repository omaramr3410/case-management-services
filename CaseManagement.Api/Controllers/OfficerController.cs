using CaseManagement.Api.Application.Orchestrators;
using CaseManagement.Api.Domain.DTOs.Officers;
using CaseManagement.Api.Infrastructure.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CaseManagement.Api.Controllers;

[ApiController]
[Route("api/officers")]
[Authorize]
public sealed class OfficerController : ControllerBase
{
    private readonly IOfficerOrchestrator _orchestrator;

    public OfficerController(IOfficerOrchestrator orchestrator)
    {
        _orchestrator = orchestrator;
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<OfficerDto>> GetById(Guid id)
    {
        return Ok(await _orchestrator.GetByIdAsync(id));
    }

    [HttpPost("search")]
    public async Task<ActionResult<IReadOnlyList<OfficerDto>>> Search(
        [FromBody] OfficerQueryRequest filter)
    {
        return Ok(await _orchestrator.SearchAsync(filter));
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Officer")]
    public async Task<ActionResult<Guid>> Create(OfficerCreateRequest request)
    {
        var user = UserContext.FromClaims(User, HttpContext.Connection.RemoteIpAddress?.ToString());
        return Ok(await _orchestrator.CreateAsync(request, user));
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Admin,Officer")]
    public async Task<IActionResult> Update(Guid id, UpdateOfficerRequest request)
    {
        var user = UserContext.FromClaims(User, HttpContext.Connection.RemoteIpAddress?.ToString());
        await _orchestrator.UpdateAsync(id, request, user);
        return NoContent();
    }
}
