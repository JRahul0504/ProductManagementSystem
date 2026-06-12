using ProductManagementSystem.Application.DTOs.Auth;

namespace ProductManagementSystem.Application.Interfaces.Services;

/// <summary>
/// Defines application operations for authentication and token lifecycle management.
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Authenticates a user and issues tokens.
    /// </summary>
    /// <param name="request">The login request.</param>
    /// <param name="cancellationToken">A token used to cancel the operation.</param>
    /// <returns>The authentication response.</returns>
    Task<ApiResponse<AuthResponseDto>> LoginAsync(
        LoginRequestDto request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Registers a new application user.
    /// </summary>
    /// <param name="request">The registration request.</param>
    /// <param name="cancellationToken">A token used to cancel the operation.</param>
    /// <returns>The created user response.</returns>
    Task<ApiResponse<UserDto>> RegisterAsync(
        RegisterRequestDto request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Rotates a refresh token and issues new tokens.
    /// </summary>
    /// <param name="request">The refresh token request.</param>
    /// <param name="cancellationToken">A token used to cancel the operation.</param>
    /// <returns>The authentication response.</returns>
    Task<ApiResponse<AuthResponseDto>> RefreshTokenAsync(
        RefreshTokenRequestDto request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Revokes a refresh token.
    /// </summary>
    /// <param name="request">The revocation request.</param>
    /// <param name="revokedBy">The user or system that revoked the token.</param>
    /// <param name="cancellationToken">A token used to cancel the operation.</param>
    /// <returns>The operation response.</returns>
    Task<ApiResponse<bool>> RevokeRefreshTokenAsync(
        RevokeRefreshTokenRequestDto request,
        string revokedBy,
        CancellationToken cancellationToken = default);
}
