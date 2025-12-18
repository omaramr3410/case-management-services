using CaseManagement.Api.DTOs;
using CaseManagement.Api.Infrastructure.Auditing;
using CaseManagement.Api.Infrastructure.Data;
using CaseManagement.Api.Infrastructure.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CaseManagement.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly JwtTokenService _jwt;
        private readonly IAuditService _audit;

        public AuthController(AppDbContext db, JwtTokenService jwt, IAuditService audit)
        {
            _db = db;
            _jwt = jwt;
            _audit = audit;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var user = _db.User
                .SingleOrDefault(u =>
                    u.Username == request.Username &&
                    u.IsActive);

            if (user == null ||
                !BCrypt.Net.BCrypt.Verify(
                    request.Password,
                    user.PasswordHash))
            {
                return Unauthorized("Invalid credentials");
            }

            var token = _jwt.GenerateToken(user);

            var userContext = UserContext.FromClaims(User);

            await _audit.LogAsync(userContext, "Login", user.Id, "View");
            await _db.SaveChangesAsync();

            return Ok(new LoginResponse
            {
                Token = token,
                Role = user.Role
            });
        }
    }

}