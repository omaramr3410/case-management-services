namespace CaseManagement.Api.Domain.Entities
{
    /// <summary>
    /// DB record definition of User entity
    /// </summary>
    public record User
    {
        public Guid Id { get; set; }

        public string Username { get; set; } = null!;

        public string PasswordHash { get; set; } = null!;

        public string Role { get; set; } = null!;

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; }

    }
}