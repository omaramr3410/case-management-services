namespace CaseManagement.Api.Domain.DTOs.Clients
{
    /// <summary>
    /// Dto to request client by filter
    /// </summary>
    public record ClientQueryRequest
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Region { get; set; }

        public string? Status { get; set; }

        public string? SSN { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string? Phone { get; set; }

        public string? Address { get; set; }

        public DateTime? CreatedAt { get; set; }
    }
}