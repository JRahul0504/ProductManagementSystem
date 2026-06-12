using ProductManagementSystem.Application.DTOs.Auth;
using ProductManagementSystem.Application.Interfaces.Security;
using ProductManagementSystem.Application.Interfaces.Services;

namespace ProductManagementSystem.Application.Services;

/// <summary>
/// Provides authentication application operations.
/// </summary>
public sealed class AuthService(
    IUnitOfWork unitOfWork,
    IMapper mapper,
    IJwtTokenService jwtTokenService,
    IPasswordHasher passwordHasher) : IAuthService
{
    /// <inheritdoc />
    public async Task<ApiResponse<AuthResponseDto>> LoginAsync(
        LoginRequestDto request,
        CancellationToken cancellationToken = default)
    {
        var user = await unitOfWork.Users.FirstOrDefaultAsync(
            candidate =>
                candidate.IsActive &&
                (candidate.UserName == request.UserNameOrEmail || candidate.Email == request.UserNameOrEmail),
            cancellationToken);

        if (user is null || !passwordHasher.VerifyPassword(request.Password, user.PasswordHash))
        {
            return ApiResponse<AuthResponseDto>.Failure("Invalid user name, email, or password.");
        }

        var response = await IssueTokensAsync(user, null, cancellationToken);

        return ApiResponse<AuthResponseDto>.Success(response, "Login completed successfully.");
    }

    /// <inheritdoc />
    public async Task<ApiResponse<UserDto>> RegisterAsync(
        RegisterRequestDto request,
        CancellationToken cancellationToken = default)
    {
        var userExists = await unitOfWork.Users.AnyAsync(
            user => user.UserName == request.UserName || user.Email == request.Email,
            cancellationToken);

        if (userExists)
        {
            return ApiResponse<UserDto>.Failure("A user with the same user name or email already exists.");
        }

        var user = new ApplicationUser
        {
            UserName = request.UserName,
            Email = request.Email,
            PasswordHash = passwordHasher.HashPassword(request.Password),
            Role = request.Role,
            IsActive = true,
            CreatedOn = DateTime.UtcNow
        };

        await unitOfWork.Users.AddAsync(user, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ApiResponse<UserDto>.Success(
            mapper.Map<UserDto>(user),
            "User registered successfully.");
    }

    /// <inheritdoc />
    public async Task<ApiResponse<AuthResponseDto>> RefreshTokenAsync(
        RefreshTokenRequestDto request,
        CancellationToken cancellationToken = default)
    {
        var existingRefreshToken = await unitOfWork.RefreshTokens.FirstOrDefaultAsync(
            refreshToken => refreshToken.Token == request.RefreshToken,
            cancellationToken);

        if (existingRefreshToken is null || existingRefreshToken.IsRevoked || existingRefreshToken.IsExpired)
        {
            return ApiResponse<AuthResponseDto>.Failure("Refresh token is invalid or expired.");
        }

        var user = await unitOfWork.Users.GetByIdAsync(
            existingRefreshToken.ApplicationUserId,
            cancellationToken);

        if (user is null || !user.IsActive)
        {
            return ApiResponse<AuthResponseDto>.Failure("Refresh token user is invalid.");
        }

        var response = await IssueTokensAsync(user, existingRefreshToken, cancellationToken);

        return ApiResponse<AuthResponseDto>.Success(response, "Token refreshed successfully.");
    }

    /// <inheritdoc />
    public async Task<ApiResponse<bool>> RevokeRefreshTokenAsync(
        RevokeRefreshTokenRequestDto request,
        string revokedBy,
        CancellationToken cancellationToken = default)
    {
        var refreshToken = await unitOfWork.RefreshTokens.FirstOrDefaultAsync(
            token => token.Token == request.RefreshToken,
            cancellationToken);

        if (refreshToken is null)
        {
            return ApiResponse<bool>.Failure("Refresh token was not found.");
        }

        if (refreshToken.IsRevoked)
        {
            return ApiResponse<bool>.Success(true, "Refresh token was already revoked.");
        }

        refreshToken.RevokedOn = DateTime.UtcNow;
        refreshToken.RevokedBy = revokedBy;
        refreshToken.RevocationReason = request.Reason ?? "Revoked by request.";

        unitOfWork.RefreshTokens.Update(refreshToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ApiResponse<bool>.Success(true, "Refresh token revoked successfully.");
    }

    private async Task<AuthResponseDto> IssueTokensAsync(
        ApplicationUser user,
        RefreshToken? tokenToReplace,
        CancellationToken cancellationToken)
    {
        var accessToken = jwtTokenService.GenerateAccessToken(user);
        var refreshTokenValue = jwtTokenService.GenerateRefreshToken();
        var accessTokenExpiresOn = jwtTokenService.GetAccessTokenExpiry();
        var refreshTokenExpiresOn = jwtTokenService.GetRefreshTokenExpiry();

        if (tokenToReplace is not null)
        {
            tokenToReplace.RevokedOn = DateTime.UtcNow;
            tokenToReplace.RevokedBy = user.UserName;
            tokenToReplace.ReplacedByToken = refreshTokenValue;
            tokenToReplace.RevocationReason = "Replaced by refresh token rotation.";
            unitOfWork.RefreshTokens.Update(tokenToReplace);
        }

        var refreshToken = new RefreshToken
        {
            ApplicationUserId = user.Id,
            Token = refreshTokenValue,
            ExpiresOn = refreshTokenExpiresOn,
            CreatedOn = DateTime.UtcNow,
            CreatedBy = user.UserName
        };

        await unitOfWork.RefreshTokens.AddAsync(refreshToken, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new AuthResponseDto
        {
            User = mapper.Map<UserDto>(user),
            AccessToken = accessToken,
            RefreshToken = refreshTokenValue,
            AccessTokenExpiresOn = accessTokenExpiresOn,
            RefreshTokenExpiresOn = refreshTokenExpiresOn
        };
    }
}
