using CaseManagement.Api.Application.Orchestrators;
using CaseManagement.Api.Domain.DTOs.Clients;
using CaseManagement.Api.DTOs.Clients;
using CaseManagement.Api.Infrastructure.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CaseManagement.Api.Controllers;

[ApiController]
[Route("api/clients")]
[Authorize]
public sealed class ClientController : ControllerBase
{
    private readonly IClientOrchestrator _clientOrchestrator;

    public ClientController(IClientOrchestrator clientOrchestrator)
    {
        _clientOrchestrator = clientOrchestrator;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ClientResponse>> GetById(int id)
    {
        return Ok(await _clientOrchestrator.GetByIdAsync(id));
    }

    [HttpPost("search")]
    public async Task<ActionResult<IReadOnlyList<ClientResponse>>> Search(
        [FromBody] ClientQueryRequest filter)
    {
        return Ok(await _clientOrchestrator.SearchAsync(filter));
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Officer")]
    public async Task<ActionResult<int>> Create(CreateClientRequest request)
    {
        var user = UserContext.FromClaims(User, HttpContext.Connection.RemoteIpAddress?.ToString());
        return Ok(await _clientOrchestrator.CreateAsync(request, user));
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin,Officer")]
    public async Task<IActionResult> Update(int id, UpdateClientRequest request)
    {
        var user = UserContext.FromClaims(User, HttpContext.Connection.RemoteIpAddress?.ToString());
        await _clientOrchestrator.UpdateAsync(id, request, user);
        return NoContent();
    }
}
