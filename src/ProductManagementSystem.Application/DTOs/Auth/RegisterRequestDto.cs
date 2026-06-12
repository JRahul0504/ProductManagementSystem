using ProductManagementSystem.Domain.Enums;

namespace ProductManagementSystem.Application.DTOs.Auth;

/// <summary>
/// Represents a user registration request.
/// </summary>
public sealed class RegisterRequestDto
{
    /// <summary>
    /// Gets or sets the requested user name.
    /// </summary>
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the email address.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the plain-text password submitted by the client.
    /// </summary>
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the requested role.
    /// </summary>
    public UserRole Role { get; set; } = UserRole.User;
}
