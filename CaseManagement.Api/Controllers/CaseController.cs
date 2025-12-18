using CaseManagement.Api.Entities;
using CaseManagement.Api.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using CaseManagement.Api.Infrastructure.Auditing;
using CaseManagement.Api.DTOs.Cases;
using CaseManagement.Api.Domain.DTOs.Cases;
using CaseManagement.Api.Application.Orchestrators;
using CaseManagement.Api.Infrastructure.Security;

namespace CaseManagement.Api.Controllers;

[ApiController]
[Route("api/cases")]
[Authorize]
public class CaseController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly IAuditService _audit;
    private readonly ICaseOrchestrator _caseOrchestrator;

    public CaseController(AppDbContext db, IAuditService audit, ICaseOrchestrator caseOrchestrator)
    {
        _db = db;
        _audit = audit;
        _caseOrchestrator = caseOrchestrator;
    }

    //[HttpGet("{id:guid}")]
    //[Authorize(Roles = "Admin,Officer,Auditor")]
    //public async Task<IActionResult> GetById(Guid id)
    //{
    //    var user = UserContext.FromClaims(User);

    //    var c = await _db.Case.FindAsync(id);
    //    if (c == null) return NotFound();

    //    await _audit.Log(user, "Case", c.Id, "View");
    //    await _db.SaveChangesAsync();

    //    return Ok(c);
    //}

    //[HttpGet]
    //[Authorize(Roles = "Admin,Officer,Auditor")]
    //public async Task<IActionResult> Query([FromQuery] CaseQueryRequest filter)
    //{
    //    var user = UserContext.FromClaims(User);

    //    var results = await _db.Case
    //        .Where(c =>
    //            (filter.ClientId == null || c.ClientId == filter.ClientId) &&
    //            (filter.AssignedOfficerId == null || c.AssignedOfficerId == filter.AssignedOfficerId) &&
    //            (filter.ServiceProviderId == null || c.ServiceProviderId == filter.ServiceProviderId) &&
    //            (filter.Status == null || c.Status == filter.Status) &&
    //            (filter.Region == null || c.Region == filter.Region) &&
    //            (filter.Recommendations == null || c.Recommendations!.Contains(filter.Recommendations)) &&
    //            (filter.CreatedAt == null || c.CreatedAt.Date == filter.CreatedAt.Value.Date) &&
    //            (filter.UpdatedAt == null || c.UpdatedAt!.Value.Date == filter.UpdatedAt.Value.Date)
    //        )
    //        .ToListAsync();

    //    await _audit.Log(user, "Case", "Multiple", "Query", filter);
    //    await _db.SaveChangesAsync();

    //    return Ok(results);
    //}

    //[HttpPost]
    //[Authorize(Roles = "Admin,Officer")]
    //public async Task<IActionResult> Create(CreateCaseRequest dto)
    //{
    //    var user = UserContext.FromClaims(User);

    //    var c = new Case
    //    {
    //        Id = Guid.NewGuid(),
    //        ClientId = dto.ClientId,
    //        Region = dto.Region ?? "US",
    //        Status = "Open",
    //        CreatedAt = DateTime.UtcNow
    //    };

    //    _db.Case.Add(c);
    //    await _audit.Log(user, "Case", c.Id, "Create");

    //    await _db.SaveChangesAsync();

    //    return CreatedAtAction(nameof(GetById), new { id = c.Id }, c.Id);
    //}

    //[HttpPut("{id:guid}")]
    //[Authorize(Roles = "Officer")]
    //public async Task<IActionResult> Update(Guid id, UpdateCaseRequest dto)
    //{
    //    var user = UserContext.FromClaims(User);

    //    var c = await _db.Case.FindAsync(id);
    //    if (c == null) return NotFound();

    //    c.Status ??= dto.Status;
    //    c.Recommendations ??= dto.Recommendations;
    //    c.UpdatedAt = DateTime.UtcNow;

    //    await _audit.Log(user, "Case", c.Id, "Update",
    //        new { Fields = new[] { "Status", "Recommendations" } });

    //    await _db.SaveChangesAsync();

    //    return NoContent();
    //}

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CaseResponse>> GetById(Guid id)
    {
        return Ok(await _caseOrchestrator.GetByIdAsync(id));
    }

    [HttpPost("search")]
    public async Task<ActionResult<IReadOnlyList<CaseResponse>>> Search(
        [FromBody] CaseQueryRequest filter)
    {
        return Ok(await _caseOrchestrator.SearchAsync(filter));
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Officer")]
    public async Task<ActionResult<Guid>> Create(CreateCaseRequest request)
    {
        var userContext = UserContext.FromClaims(User, HttpContext.Connection.RemoteIpAddress?.ToString());

        // return Ok(await _caseOrchestrator.CreateAsync(request, user));
        var result = await _caseOrchestrator.CreateAsync(request, userContext);

        return CreatedAtAction(nameof(GetById), new { id = result }, result);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Admin,Officer")]
    public async Task<IActionResult> Update(Guid id, UpdateCaseRequest request)
    {
        var user = UserContext.FromClaims(User, HttpContext.Connection.RemoteIpAddress?.ToString());

        await _caseOrchestrator.UpdateAsync(id, request, user);
        return NoContent();
    }
}
