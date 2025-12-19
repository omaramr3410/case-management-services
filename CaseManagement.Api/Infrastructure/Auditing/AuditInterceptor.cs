using Castle.DynamicProxy;
using CaseManagement.Api.Infrastructure.Security;

namespace CaseManagement.Api.Infrastructure.Auditing
{
    public class AuditInterceptor : IAsyncInterceptor
    {
        private readonly IAuditService _audit;
        private readonly IUserContextProvider _userContext;

        public AuditInterceptor(
            IAuditService audit,
            IUserContextProvider userContext)
        {
            _audit = audit;
            _userContext = userContext;
        }

        public void InterceptSynchronous(IInvocation invocation)
        {
            invocation.Proceed();
            Audit(invocation);
        }

        public void InterceptAsynchronous(IInvocation invocation)
        {
            invocation.Proceed();
            invocation.ReturnValue = InterceptAsync((Task)invocation.ReturnValue, invocation);
        }

        public void InterceptAsynchronous<TResult>(IInvocation invocation)
        {
            invocation.Proceed();
            invocation.ReturnValue = InterceptAsync((Task<TResult>)invocation.ReturnValue, invocation);
        }

        private async Task InterceptAsync(Task task, IInvocation invocation)
        {
            await task;
            Audit(invocation);
        }

        private async Task<TResult> InterceptAsync<TResult>(Task<TResult> task, IInvocation invocation)
        {
            var result = await task;
            Audit(invocation);
            return result;
        }

        private void Audit(IInvocation invocation)
        {
            var user = _userContext.GetUserContext();
            if (user == null) return;

            _audit.LogAsync(
                user,
                invocation.TargetType.Name,
                invocation.Method.Name,
                "EXECUTE");
        }
    }
}
