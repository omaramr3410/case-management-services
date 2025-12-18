namespace CaseManagement.Api.Domain.Entities
{
    /// <summary>
    /// Entity definition of Relational record in DB
    /// </summary>
    [Serializable]
    public record Client
    {
        /// <summary>
        /// Record's unique identifier 
        /// </summary>
        public Guid Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Region { get; set; } = null!;

        public string Status { get; set; } = null!;

        public string SSN { get; set; } = null!;

        public DateTime DateOfBirth { get; set; }

        public string? Phone { get; set; }

        public string? Address { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}