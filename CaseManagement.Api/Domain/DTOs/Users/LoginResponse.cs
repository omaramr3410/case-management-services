using CaseManagement.Api.Domain.Entities;

namespace CaseManagement.Api.Domain.DTOs.Users
{
    /// <summary>
    /// Reponse dto to provide the JWT and User role - after successfull user authentication
    /// </summary>
    public class LoginResponse
    {
        public string Token { get; set; } = null!;

        public int ExpiresMinutes { get; set; }

        public User User { get; set; } = null!;
    }
}