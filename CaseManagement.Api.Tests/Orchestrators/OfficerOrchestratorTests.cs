using CaseManagement.Api.Domain.DTOs.Officers;
using CaseManagement.Api.Domain.Entities;
using CaseManagement.Api.Infrastructure.Auditing;
using CaseManagement.Api.Infrastructure.Repositories;
using CaseManagement.Api.Infrastructure.Security;
using CaseManagement.Api.Orchestrators;
using Castle.DynamicProxy;
using NSubstitute;
using System;
using System.Threading.Tasks;
using Xunit;

namespace CaseManagement.Api.Tests.Orchestrators
{
    public class OfficerOrchestratorTests
    {
        [Fact]
        public async Task CreateAsync_Should_Audit_Via_Interceptor()
        {
            // Arrange
            var repo = Substitute.For<IOfficerRepository>();
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

            repo.CreateAsync(Arg.Any<Officer>())
                .Returns(ci => ci.Arg<Officer>().Id);

            var proxyGenerator = new ProxyGenerator();

            var auditInterceptor = new AuditInterceptor(audit, userContext);

            var repoProxy =
                proxyGenerator.CreateInterfaceProxyWithTarget<IOfficerRepository>(
                    repo,
                    auditInterceptor
                );

            var orchestrator = new OfficerOrchestrator(repoProxy, audit);

            var request = new OfficerCreateRequest
            {
                FirstName = "Jane",
                LastName = "Smith",
                Region = "VA"
            };

            // Act
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
