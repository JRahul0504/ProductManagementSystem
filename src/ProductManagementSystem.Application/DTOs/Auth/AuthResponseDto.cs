namespace ProductManagementSystem.Application.DTOs.Auth;

/// <summary>
/// Represents a successful authentication response.
/// </summary>
public sealed class AuthResponseDto
{
    /// <summary>
    /// Gets or sets the authenticated user.
    /// </summary>
    public UserDto User { get; set; } = new();

    /// <summary>
    /// Gets or sets the JWT access token.
    /// </summary>
    public string AccessToken { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the refresh token.
    /// </summary>
    public string RefreshToken { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the UTC date and time when the access token expires.
    /// </summary>
    public DateTime AccessTokenExpiresOn { get; set; }

    /// <summary>
    /// Gets or sets the UTC date and time when the refresh token expires.
    /// </summary>
    public DateTime RefreshTokenExpiresOn { get; set; }
}
