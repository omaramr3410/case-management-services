namespace CaseManagement.Api.DTOs.Clients
{
    /// <summary>
    /// Dto to define body of update Client request 
    /// </summary>
    public record UpdateClientRequest
    {
        public string? Region { get; init; }
        public string? Status { get; init; }
        public string? Phone { get; init; }
        public string? Address { get; init; }
    }
}