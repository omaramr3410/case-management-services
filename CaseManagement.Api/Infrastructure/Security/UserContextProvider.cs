namespace CaseManagement.Api.Infrastructure.Security
{
    public class TestUserContextProvider : IUserContextProvider
    {
        private readonly UserContext _context;

        public TestUserContextProvider(UserContext context)
        {
            _context = context;
        }

        public UserContext GetUserContext() => _context;
    }
}
