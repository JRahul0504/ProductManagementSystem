namespace ProductManagementSystem.Application.Interfaces.Security;

/// <summary>
/// Defines JWT and refresh token generation operations.
/// </summary>
public interface IJwtTokenService
{
    /// <summary>
    /// Creates a JWT access token for the specified user.
    /// </summary>
    /// <param name="user">The authenticated application user.</param>
    /// <returns>The generated access token.</returns>
    string GenerateAccessToken(ApplicationUser user);

    /// <summary>
    /// Creates a secure refresh token value.
    /// </summary>
    /// <returns>The generated refresh token.</returns>
    string GenerateRefreshToken();

    /// <summary>
    /// Gets the UTC date and time when a new access token expires.
    /// </summary>
    /// <returns>The access token expiry timestamp.</returns>
    DateTime GetAccessTokenExpiry();

    /// <summary>
    /// Gets the UTC date and time when a new refresh token expires.
    /// </summary>
    /// <returns>The refresh token expiry timestamp.</returns>
    DateTime GetRefreshTokenExpiry();
}
