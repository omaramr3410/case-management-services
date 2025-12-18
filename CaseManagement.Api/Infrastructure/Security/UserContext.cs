using System.Security.Claims;

namespace CaseManagement.Api.Infrastructure.Security;

public interface IUserContext
{
    Guid? UserId { get; }
    string? Username { get; }
    string? UserRole { get; }
    string? IpAddress { get; }
}

/// <summary>
/// Represents the authenticated user making the request.
/// Used for audit logging and authorization context.
/// </summary>
public sealed record UserContext : IUserContext
{
    // Actor
    public Guid? UserId { get; init; }
    public string? Username { get; init; }
    public string? UserRole { get; init; }

    // Request context
    public string? IpAddress { get; init; }

    /// <summary>
    /// Builds a UserContext from the current ClaimsPrincipal.
    /// </summary>
    public static UserContext FromClaims(ClaimsPrincipal user, string? ipAddress = null)
    {
        if (user == null || !user.Identity?.IsAuthenticated == true)
        {
            return new UserContext
            {
                IpAddress = ipAddress
            };
        }

        return new UserContext
        {
            UserId = TryGetGuidClaim(user, ClaimTypes.NameIdentifier),
            Username = user.Identity?.Name
                       ?? user.FindFirstValue(ClaimTypes.Name),
            UserRole = user.FindFirstValue(ClaimTypes.Role),
            IpAddress = ipAddress
        };
    }

    private static Guid? TryGetGuidClaim(ClaimsPrincipal user, string claimType)
    {
        var value = user.FindFirstValue(claimType);
        return Guid.TryParse(value, out var id) ? id : null;
    }
}
