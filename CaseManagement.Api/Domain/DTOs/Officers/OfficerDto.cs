namespace CaseManagement.Api.Domain.DTOs.Officers;

/// <summary>
/// Dto defining Officer record for external applications 
/// </summary>
public record OfficerDto
{
    public Guid Id { get; init; }

    public Guid UserId { get; init; }

    public string FirstName { get; init; } = null!;

    public string LastName { get; init; } = null!;

    public string Region { get; init; } = null!;

    public DateTime CreatedAt { get; init; }
}
