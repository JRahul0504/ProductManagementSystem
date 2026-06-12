namespace ProductManagementSystem.Application.DTOs.Auth;

/// <summary>
/// Represents a login request.
/// </summary>
public sealed class LoginRequestDto
{
    /// <summary>
    /// Gets or sets the email address or user name.
    /// </summary>
    public string UserNameOrEmail { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the plain-text password submitted by the client.
    /// </summary>
    public string Password { get; set; } = string.Empty;
}
