using CaseManagement.Api.Domain.DTOs.Cases;
using CaseManagement.Api.DTOs.Cases;
using CaseManagement.Api.Infrastructure.Auditing;
using CaseManagement.Api.Infrastructure.Repositories;
using CaseManagement.Api.Infrastructure.Security;
using CaseManagement.Api.Orchestrators;
using NSubstitute;
using Xunit;

public class CaseOrchestratorTests
{
    private readonly ICaseRepository _repo = Substitute.For<ICaseRepository>();
    private readonly IAuditService _audit = Substitute.For<IAuditService>();

    [Fact]
    public async Task CreateAsync_ShouldAssignGuidAndAudit()
    {
        var orchestrator = new CaseOrchestrator(_repo, _audit);

        var request = new CreateCaseRequest
        {
            ClientId = Guid.NewGuid(),
            Status = "Open",
            Region = "VA"
        };

        var userContext = new UserContext { UserId = Guid.Parse("CD1EF5BF-3BD4-49F9-1FB5-08DE3CDD3A2C"), Username = "adminTestRole", IpAddress = "TESTING", UserRole = "admin" };

        var result = await orchestrator.CreateAsync(request, userContext);

        Assert.NotEqual(Guid.Empty, result);
        await _audit.Received(1).LogAsync(userContext, "Case", result.ToString(), "CREATE");
    }
}
