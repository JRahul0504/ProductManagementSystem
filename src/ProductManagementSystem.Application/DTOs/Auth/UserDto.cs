using ProductManagementSystem.Domain.Enums;

namespace ProductManagementSystem.Application.DTOs.Auth;

/// <summary>
/// Represents safe application user data returned to API clients.
/// </summary>
public sealed class UserDto
{
    /// <summary>
    /// Gets or sets the user identifier.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the user name.
    /// </summary>
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the email address.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the user role.
    /// </summary>
    public UserRole Role { get; set; }
}
