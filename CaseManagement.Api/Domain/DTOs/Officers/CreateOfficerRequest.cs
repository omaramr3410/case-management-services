using System.ComponentModel.DataAnnotations;

namespace CaseManagement.Api.Domain.DTOs.Officers;

/// <summary>
/// 
/// </summary>
public record OfficerCreateRequest
{
    [Required]
    public Guid UserId { get; set; }
    public string FirstName { get; init; } = null!;
    public string LastName { get; init; } = null!;
    public string Region { get; init; } = null!;
    public string Role { get; init; } = "Officer";
}
