using System.ComponentModel.DataAnnotations;

namespace CaseManagement.Api.DTOs
{
    /// <summary>
    /// Request dto to initiate authentication process and possibly return JWT 
    /// </summary>
    public record LoginRequest
    {
        [Required]
        public string Username { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }
}