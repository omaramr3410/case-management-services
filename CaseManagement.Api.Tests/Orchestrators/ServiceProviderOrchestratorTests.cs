using CaseManagement.Api.Application.Orchestrators;
using CaseManagement.Api.Domain.DTOs.ServiceProviders;
using CaseManagement.Api.Infrastructure.Auditing;
using CaseManagement.Api.Infrastructure.Repositories;
using ServiceProvider = CaseManagement.Api.Domain.Entities.ServiceProvider;
using NSubstitute;
using CaseManagement.Api.Infrastructure.Security;

namespace CaseManagement.Api.Tests.Orchestrators
{
    public class ServiceProviderOrchestratorTests
    {
        private readonly IServiceProviderRepository _repo = Substitute.For<IServiceProviderRepository>();
        private readonly IAuditService _audit = Substitute.For<IAuditService>();

        [Fact]
        public async Task CreateAsync_ShouldCreateServiceProvider()
        {
            // Arrange
            var orchestrator = new ServiceProviderOrchestrator(_repo, _audit);

            var request = new CrreateServiceProviderRequest
            {
                Name = "Provider A",
                Region = "VA",
                ServiceType = "Medical"
            };

            var userContext = new UserContext { UserId = Guid.Parse("CD1EF5BF-3BD4-49F9-1FB5-08DE3CDD3A2C"), Username = "adminTestRole", IpAddress = "TESTING", UserRole = "admin" };

            // Act
            var result = await orchestrator.CreateAsync(request, userContext);

            // Assert
            Assert.NotEqual(Guid.Empty, result.Id);
            await _repo.Received(1).CreateAsync(Arg.Any<ServiceProvider>());
            await _audit.Received(1).LogAsync(userContext, "ServiceProvider", result.Id.ToString(), "CREATE");
        }
    }
}