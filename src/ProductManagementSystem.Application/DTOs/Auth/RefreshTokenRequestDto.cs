namespace ProductManagementSystem.Application.DTOs.Auth;

/// <summary>
/// Represents a refresh token request.
/// </summary>
public sealed class RefreshTokenRequestDto
{
    /// <summary>
    /// Gets or sets the refresh token issued to the client.
    /// </summary>
    public string RefreshToken { get; set; } = string.Empty;
}
