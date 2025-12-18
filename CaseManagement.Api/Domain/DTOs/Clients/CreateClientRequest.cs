namespace CaseManagement.Api.DTOs.Clients
{
    /// <summary>
    /// Dto to define body of create client request 
    /// </summary>
    public record CreateClientRequest
    {
        public string FirstName { get; init; } = null!;
        public string LastName { get; init; } = null!;
        public string Region { get; init; } = null!;
        public string Status { get; init; } = null!;
        public string SSN { get; init; } = null!;
        public DateTime DateOfBirth { get; init; }
        public string? Phone { get; init; }
        public string? Address { get; init; }
    }
}