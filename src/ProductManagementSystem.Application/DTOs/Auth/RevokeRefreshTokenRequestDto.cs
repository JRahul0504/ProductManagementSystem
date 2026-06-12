namespace ProductManagementSystem.Application.DTOs.Auth;

/// <summary>
/// Represents a refresh token revocation request.
/// </summary>
public sealed class RevokeRefreshTokenRequestDto
{
    /// <summary>
    /// Gets or sets the refresh token to revoke.
    /// </summary>
    public string RefreshToken { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the reason for revocation.
    /// </summary>
    public string? Reason { get; set; }
}
