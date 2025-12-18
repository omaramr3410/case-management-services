namespace CaseManagement.Api.Domain.DTOs.Clients
{
    /// <summary>
    /// Defines dto to external applications
    /// </summary>
    public record ClientDto
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
