using CaseManagement.Api.Entities;

namespace CaseManagement.Api.Domain.DTOs.Cases
{
    /// <summary>
    /// Dto to contain details of Case 
    /// </summary>
    public record CaseDto
    {
        public Guid Id { get; set; }

        public Guid ClientId { get; set; }

        public Guid? AssignedOfficerId { get; set; }

        public Guid? ServiceProviderId { get; set; }

        public string Status { get; set; } = null!;
        public string Region { get; set; } = null!;
        public string? Recommendations { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    };
}
