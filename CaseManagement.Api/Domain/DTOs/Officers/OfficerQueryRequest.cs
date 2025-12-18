namespace CaseManagement.Api.Domain.DTOs.Officers;

/// <summary>
/// 
/// </summary>
public record OfficerQueryRequest
{
    public Guid? UserId { get; set; }
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public string? Region { get; init; }
}
