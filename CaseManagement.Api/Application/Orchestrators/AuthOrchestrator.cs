using CaseManagement.Api.Domain.Entities;
using CaseManagement.Api.DTOs;
using CaseManagement.Api.Infrastructure.Data;
using CaseManagement.Api.Infrastructure.Security;

namespace CaseManagement.Api.Application.Orchestrators
{
    public class AuthOrchestrator : IAuthOrchestrator
    {
        private readonly AppDbContext _db;
        private readonly PasswordHasher _hasher;
        private readonly JwtTokenService _jwt;

        public AuthOrchestrator(
            AppDbContext db,
            PasswordHasher hasher,
            JwtTokenService jwt)
        {
            _db = db;
            _hasher = hasher;
            _jwt = jwt;
        }

        public User? Authenticate(LoginRequest request)
        {
            var user = _db.User.SingleOrDefault(u => u.Username == request.Username);

            if (user == null || !_hasher.Verify(request.Password, user.PasswordHash))
                return null;

            return user;
        }
    }
}