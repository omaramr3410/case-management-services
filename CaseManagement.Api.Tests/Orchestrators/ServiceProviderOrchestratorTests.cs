using CaseManagement.Api.Application.Orchestrators;
using CaseManagement.Api.Domain.DTOs.ServiceProviders;
using CaseManagement.Api.Domain.Entities;
using CaseManagement.Api.Infrastructure.Auditing;
using CaseManagement.Api.Infrastructure.Repositories;
using CaseManagement.Api.Infrastructure.Security;
using Castle.DynamicProxy;
using NSubstitute;
using System;
using System.Threading.Tasks;
using Xunit;
using ServiceProvider = CaseManagement.Api.Domain.Entities.ServiceProvider;

namespace CaseManagement.Api.Tests.Orchestrators
{
    public class ServiceProviderOrchestratorTests
    {
        [Fact]
        public async Task CreateAsync_Should_Audit_Via_Interceptor()
        {
            // Arrange
            var repo = Substitute.For<IServiceProviderRepository>();
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

            repo.CreateAsync(Arg.Any<ServiceProvider>())
                .Returns(call =>
                    Task.FromResult(call.Arg<ServiceProvider>().Id)
                );


            var proxyGenerator = new ProxyGenerator();

            var auditInterceptor = new AuditInterceptor(audit, userContext);

            var repoProxy =
                proxyGenerator.CreateInterfaceProxyWithTarget<IServiceProviderRepository>(
                    repo,
                    auditInterceptor
                );

            var orchestrator = new ServiceProviderOrchestrator(repoProxy, audit);

            var request = new CreateServiceProviderRequest
            {
                Name = "Test Provider",
                Region = "VA"
            };

            // Act
            var result = await orchestrator.CreateAsync(request, user);

            // Assert
            Assert.NotEqual(Guid.Empty, result.Id);

            await audit.Received(1).LogAsync(
                Arg.Any<UserContext>(),
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<string>());
        }
    }
}
