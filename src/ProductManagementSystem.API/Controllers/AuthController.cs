using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using ProductManagementSystem.Application.DTOs.Auth;
using ProductManagementSystem.Application.Interfaces.Services;

namespace ProductManagementSystem.API.Controllers;

/// <summary>
/// Provides authentication and refresh token endpoints.
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/auth")]
public sealed class AuthController(IAuthService authService) : ControllerBase
{
    /// <summary>
    /// Authenticates a user and returns access and refresh tokens.
    /// </summary>
    /// <param name="request">The login request.</param>
    /// <param name="cancellationToken">A token used to cancel the operation.</param>
    /// <returns>The authentication response.</returns>
    [HttpPost("login")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ApiResponse<AuthResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<AuthResponseDto>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login(
        [FromBody] LoginRequestDto request,
        CancellationToken cancellationToken)
    {
        var response = await authService.LoginAsync(request, cancellationToken);

        return response.Succeeded
            ? Ok(response)
            : BadRequest(response);
    }

    /// <summary>
    /// Registers a new application user.
    /// </summary>
    /// <param name="request">The registration request.</param>
    /// <param name="cancellationToken">A token used to cancel the operation.</param>
    /// <returns>The created user response.</returns>
    [HttpPost("register")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ApiResponse<UserDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<UserDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Register(
        [FromBody] RegisterRequestDto request,
        CancellationToken cancellationToken)
    {
        var response = await authService.RegisterAsync(request, cancellationToken);

        return response.Succeeded
            ? StatusCode(StatusCodes.Status201Created, response)
            : BadRequest(response);
    }

    /// <summary>
    /// Rotates a valid refresh token and returns a new token pair.
    /// </summary>
    /// <param name="request">The refresh token request.</param>
    /// <param name="cancellationToken">A token used to cancel the operation.</param>
    /// <returns>The refreshed authentication response.</returns>
    [HttpPost("refresh-token")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ApiResponse<AuthResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<AuthResponseDto>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RefreshToken(
        [FromBody] RefreshTokenRequestDto request,
        CancellationToken cancellationToken)
    {
        var response = await authService.RefreshTokenAsync(request, cancellationToken);

        return response.Succeeded
            ? Ok(response)
            : BadRequest(response);
    }

    /// <summary>
    /// Revokes an active refresh token.
    /// </summary>
    /// <param name="request">The refresh token revocation request.</param>
    /// <param name="cancellationToken">A token used to cancel the operation.</param>
    /// <returns>The revocation response.</returns>
    [HttpPost("revoke-token")]
    [Authorize(Roles = "User,Admin")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> RevokeToken(
        [FromBody] RevokeRefreshTokenRequestDto request,
        CancellationToken cancellationToken)
    {
        var revokedBy = User.Identity?.Name ?? "system";
        var response = await authService.RevokeRefreshTokenAsync(request, revokedBy, cancellationToken);

        return response.Succeeded
            ? Ok(response)
            : BadRequest(response);
    }
}
