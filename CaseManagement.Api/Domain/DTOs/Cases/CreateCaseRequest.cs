using System.ComponentModel.DataAnnotations;

namespace CaseManagement.Api.Domain.DTOs.Cases
{
    /// <summary>
    /// Request dto to create a new case 
    /// </summary>
    public record CreateCaseRequest
    {
        [Required]
        public Guid ClientId { get; set; }

        public Guid? AssignedOfficerId { get; set; }

        public Guid? ServiceProviderId { get; set; }

        public string? Status { get; set; }

        public string? Region { get; set; }

        public string? Recommendations { get; set; }
    }
}