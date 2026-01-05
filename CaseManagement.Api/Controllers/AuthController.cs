using CaseManagement.Api.Application.Orchestrators;
using CaseManagement.Api.Domain.DTOs.Users;
using CaseManagement.Api.DTOs;
using CaseManagement.Api.Infrastructure.Auditing;
using CaseManagement.Api.Infrastructure.Data;
using CaseManagement.Api.Infrastructure.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CaseManagement.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly JwtTokenService _jwt;
        private readonly IAuditService _audit;
        private readonly IAuthOrchestrator _authOrchestrator;
        private readonly JwtOptions _options;

        public AuthController(AppDbContext db, JwtTokenService jwt, IAuditService audit, IAuthOrchestrator authOrchestrator, IOptions<JwtOptions> options)
        {
            _db = db;
            _jwt = jwt;
            _audit = audit;
            _authOrchestrator = authOrchestrator;
            _options = options.Value;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var user = this._authOrchestrator.Authenticate(request);

            if (user == null)
            {
                // throw new UnauthorizedAccessException("Invalid credentials");
                return Unauthorized("Invalid credentials");
            }

            var token = _jwt.GenerateToken(user);
            var expiresMinutes = _options.ExpiresMinutes;

            var userContext = UserContext.FromClaims(User);

            await _audit.LogAsync(userContext, "Login", user.Id, "View");
            await _db.SaveChangesAsync();

            return Ok( new LoginResponse
            {
                Token = token,
                ExpiresMinutes = expiresMinutes,
                User = user
            });
        }
    }

}