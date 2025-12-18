using CaseManagement.Api.Application.Orchestrators;
using CaseManagement.Api.DTOs.Clients;
using CaseManagement.Api.Infrastructure.Auditing;
using CaseManagement.Api.Infrastructure.Repositories;
using CaseManagement.Api.Infrastructure.Security;
using CaseManagement.Api.Orchestrators;
using NSubstitute;
using Xunit;

public class ClientOrchestratorTests
{
    private readonly IClientRepository _repo = Substitute.For<IClientRepository>();
    private readonly IAuditService _audit = Substitute.For<IAuditService>();

    [Fact]
    public async Task CreateAsync_ShouldAuditClientCreation()
    {
        var orchestrator = new ClientOrchestrator(_repo, _audit);

        var request = new CreateClientRequest
        {
            FirstName = "John",
            LastName = "Doe",
            Region = "VA",
            Status = "Active",
            SSN = "123-45-6789",
            DateOfBirth = DateTime.UtcNow.AddYears(-30)
        };

        var userContext = new UserContext { UserId = Guid.Parse("CD1EF5BF-3BD4-49F9-1FB5-08DE3CDD3A2C"), Username = "adminTestRole", IpAddress = "TESTING", UserRole = "admin" };

        var result = await orchestrator.CreateAsync(request, userContext);

        Assert.NotNull(result);
        await _audit.Received(1).LogAsync(userContext, "Client", result.ToString(), "CREATE");
    }
}
