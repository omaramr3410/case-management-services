namespace CaseManagement.Api.DTOs.Cases
{
    /// <summary>
    /// Record definition of UpdateCase request
    /// </summary>
    [Serializable]
    public record UpdateCaseRequest
    {
        public Guid? AssignedOfficerId { get; set; }

        public Guid? ServiceProviderId { get; set; }

        public string? Status { get; set; }

        public string? Region { get; set; }

        public string? Recommendations { get; set; }
    }
}