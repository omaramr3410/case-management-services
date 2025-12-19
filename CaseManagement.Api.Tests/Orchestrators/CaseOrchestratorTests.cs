using CaseManagement.Api.Domain.DTOs.Cases;
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
    public class CaseOrchestratorTests
    {
        [Fact]
        public async Task CreateAsync_Should_Audit_Via_Interceptor()
        {
            // Arrange
            var repo = Substitute.For<ICaseRepository>();
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

            repo.CreateAsync(Arg.Any<Case>())
                .Returns(ci => ci.Arg<Case>().Id);

            var proxyGenerator = new ProxyGenerator();

            var auditInterceptor = new AuditInterceptor(audit, userContext);

            var repoProxy =
                proxyGenerator.CreateInterfaceProxyWithTarget<ICaseRepository>(
                    repo,
                    auditInterceptor
                );

            var orchestrator = new CaseOrchestrator(repoProxy, audit);

            var request = new CreateCaseRequest
            {
                ClientId = Guid.NewGuid(),
                Status = "Open",
                Region = "VA"
            };

            var id = await orchestrator.CreateAsync(request, user);

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
