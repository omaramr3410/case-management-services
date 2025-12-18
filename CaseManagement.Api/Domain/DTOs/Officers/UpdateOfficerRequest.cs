namespace CaseManagement.Api.Domain.DTOs.Officers;

/// <summary>
/// 
/// </summary>
public record UpdateOfficerRequest
{
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public string? Region { get; init; }
    public string? Role { get; init; }
    public bool? IsActive { get; init; }
}
