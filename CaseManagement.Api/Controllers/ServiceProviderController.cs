using CaseManagement.Api.Application.Orchestrators;
using CaseManagement.Api.Domain.DTOs.ServiceProviders;
using CaseManagement.Api.Infrastructure.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CaseManagement.Api.Controllers
{
    [ApiController]
    [Route("api/service-providers")]
    [Authorize(Roles = "Admin,Officer")]
    public class ServiceProvidersController : ControllerBase
    {
        private readonly IServiceProviderOrchestrator _serviceProvierOrchestrator;

        public ServiceProvidersController(IServiceProviderOrchestrator serviceProviderOrchestrator)
        {
            _serviceProvierOrchestrator = serviceProviderOrchestrator;
        }


        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
            => (await _serviceProvierOrchestrator.GetByIdAsync(id)) is { } result ? Ok(result) : NotFound();

        [HttpPost("search")]
        public async Task<IActionResult> Search(ServiceProviderQueryRequest request)
        {

            return Ok(await _serviceProvierOrchestrator.SearchAsync(request));
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Officer")]
        public async Task<IActionResult> Create(CreateServiceProviderRequest request)
        {
            var user = UserContext.FromClaims(User, HttpContext.Connection.RemoteIpAddress?.ToString());
            // CreatedAtAction(nameof(GetById), new { id = (await _orchestrator.CreateAsync(request)).Id },
            //     await _orchestrator.CreateAsync(request));

            return Ok(await _serviceProvierOrchestrator.CreateAsync(request, user));
        }
    }
}
