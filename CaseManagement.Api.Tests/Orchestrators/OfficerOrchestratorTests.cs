using CaseManagement.Api.Domain.DTOs.Officers;
using CaseManagement.Api.Infrastructure.Auditing;
using CaseManagement.Api.Infrastructure.Repositories;
using CaseManagement.Api.Infrastructure.Security;
using CaseManagement.Api.Orchestrators;
using NSubstitute;

namespace CaseManagement.Api.Tests.Orchestrators
{
    public class OfficerOrchestratorTests
    {
        private readonly IOfficerRepository _repo = Substitute.For<IOfficerRepository>();
        private readonly IAuditService _audit = Substitute.For<IAuditService>();

        [Fact]
        public async Task CreateAsync_ShouldCreateOfficer()
        {
            var orchestrator = new OfficerOrchestrator(_repo, _audit);

            var request = new OfficerCreateRequest
            {
                FirstName = "Jane",
                LastName = "Smith",
                Region = "VA"
            };

            var userContext = new UserContext { UserId = Guid.Parse("CD1EF5BF-3BD4-49F9-1FB5-08DE3CDD3A2C"), Username = "adminTestRole", IpAddress = "TESTING", UserRole = "admin" };

            var result = await orchestrator.CreateAsync(request, userContext);
            await _audit.Received(1).LogAsync(userContext, "Officer", result.ToString(), "CREATE");
        }
    }
}