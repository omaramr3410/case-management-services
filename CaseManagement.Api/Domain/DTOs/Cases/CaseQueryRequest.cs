namespace CaseManagement.Api.DTOs.Cases
{
    /// <summary>
    /// Record definition of CaseQuery filter body
    /// </summary>
    [Serializable]
    public record CaseQueryRequest
    {
        public Guid? ClientId { get; set; }

        public Guid? AssignedOfficerId { get; set; }

        public Guid? ServiceProviderId { get; set; }

        public string? Status { get; set; }

        public string? Region { get; set; }

        public string? Recommendations { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}