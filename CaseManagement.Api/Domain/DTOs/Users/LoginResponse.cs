namespace CaseManagement.Api.DTOs
{
    /// <summary>
    /// Reponse dto to provide the JWT and User role - after successfull user authentication
    /// </summary>
    public class LoginResponse
    {
        public string Token { get; set; } = null!;
        public string Role { get; set; } = null!;
    }
}