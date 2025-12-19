using CaseManagement.Api.Application.Orchestrators;
using CaseManagement.Api.Domain.Entities;
using CaseManagement.Api.DTOs.Clients;
using CaseManagement.Api.Infrastructure.Auditing;
using CaseManagement.Api.Infrastructure.Repositories;
using CaseManagement.Api.Infrastructure.Security;
using Castle.DynamicProxy;
using NSubstitute;
using System;
using System.Threading.Tasks;
using Xunit;

namespace CaseManagement.Api.Tests.Orchestrators
{
    public class ClientOrchestratorTests
    {
        [Fact]
        public async Task CreateAsync_Should_Audit_Via_Interceptor()
        {
            // Arrange
            var repo = Substitute.For<IClientRepository>();
            var audit = Substitute.For<IAuditService>();
            var userContext = Substitute.For<IUserContextProvider>();

            var user = new UserContext
            {
                UserId = Guid.NewGuid(),
                Username = "tester",
                UserRole = "admin",
                IpAddress = "127.0.0.1"
            };

            userContext.GetUserContext().Returns(user);

            repo.CreateAsync(Arg.Any<Client>())
                .Returns(ci => ci.Arg<Client>().Id);

            var proxyGenerator = new ProxyGenerator();

            var auditInterceptor = new AuditInterceptor(audit, userContext);

            var repoProxy =
                proxyGenerator.CreateInterfaceProxyWithTarget(
                    repo,
                    auditInterceptor
                );

            var orchestrator = new ClientOrchestrator(repoProxy, audit);

            var request = new CreateClientRequest
            {
                FirstName = "John",
                LastName = "Doe",
                Region = "VA",
                Status = "Active",
                SSN = "123-45-6789",
                DateOfBirth = DateTime.UtcNow.AddYears(-30)
            };

            // Act [Create]
            var id = await orchestrator.CreateAsync(request, user);

            // Assert
            Assert.NotEqual(Guid.Empty, id);

            await audit.Received(1).LogAsync(
                Arg.Any<UserContext>(),
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<string>());
        }
    }
}
