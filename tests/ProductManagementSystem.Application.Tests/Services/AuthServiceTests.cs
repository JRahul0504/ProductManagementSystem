using FluentAssertions;
using Moq;
using ProductManagementSystem.Application.DTOs.Auth;
using ProductManagementSystem.Application.Interfaces.Persistence;
using ProductManagementSystem.Application.Interfaces.Security;
using ProductManagementSystem.Application.Services;
using ProductManagementSystem.Application.Tests.TestSupport;
using ProductManagementSystem.Domain.Entities;
using ProductManagementSystem.Domain.Enums;
using System.Linq.Expressions;

namespace ProductManagementSystem.Application.Tests.Services;

public sealed class AuthServiceTests
{
    [Fact]
    public async Task LoginAsync_ReturnsTokenResponse_WhenCredentialsAreValid()
    {
        var user = new ApplicationUser
        {
            Id = 1,
            UserName = "admin",
            Email = "admin@test.local",
            PasswordHash = "hash",
            Role = UserRole.Admin,
            IsActive = true
        };
        var users = new Mock<IGenericRepository<ApplicationUser>>();
        users.Setup(repository => repository.FirstOrDefaultAsync(It.IsAny<Expression<Func<ApplicationUser, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);
        var refreshTokens = new Mock<IGenericRepository<RefreshToken>>();
        var unitOfWork = CreateUnitOfWork(users.Object, refreshTokens.Object);
        var passwordHasher = new Mock<IPasswordHasher>();
        passwordHasher.Setup(hasher => hasher.VerifyPassword("Password123!", "hash")).Returns(true);
        var tokenService = CreateTokenService();
        var service = new AuthService(unitOfWork.Object, TestMapper.Create(), tokenService.Object, passwordHasher.Object);

        var response = await service.LoginAsync(new LoginRequestDto
        {
            UserNameOrEmail = "admin",
            Password = "Password123!"
        });

        response.Succeeded.Should().BeTrue();
        response.Data!.AccessToken.Should().Be("access-token");
        response.Data.RefreshToken.Should().Be("refresh-token");
        refreshTokens.Verify(repository => repository.AddAsync(It.IsAny<RefreshToken>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task RefreshTokenAsync_RotatesRefreshToken_WhenTokenIsActive()
    {
        var user = new ApplicationUser
        {
            Id = 1,
            UserName = "admin",
            Email = "admin@test.local",
            PasswordHash = "hash",
            Role = UserRole.Admin,
            IsActive = true
        };
        var existingToken = new RefreshToken
        {
            Id = 1,
            ApplicationUserId = user.Id,
            Token = "old-refresh-token",
            CreatedBy = user.UserName,
            CreatedOn = DateTime.UtcNow,
            ExpiresOn = DateTime.UtcNow.AddDays(1)
        };
        var users = new Mock<IGenericRepository<ApplicationUser>>();
        users.Setup(repository => repository.GetByIdAsync(user.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);
        var refreshTokens = new Mock<IGenericRepository<RefreshToken>>();
        refreshTokens.Setup(repository => repository.FirstOrDefaultAsync(It.IsAny<Expression<Func<RefreshToken, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingToken);
        var unitOfWork = CreateUnitOfWork(users.Object, refreshTokens.Object);
        var service = new AuthService(unitOfWork.Object, TestMapper.Create(), CreateTokenService().Object, Mock.Of<IPasswordHasher>());

        var response = await service.RefreshTokenAsync(new RefreshTokenRequestDto
        {
            RefreshToken = existingToken.Token
        });

        response.Succeeded.Should().BeTrue();
        existingToken.IsRevoked.Should().BeTrue();
        existingToken.ReplacedByToken.Should().Be("refresh-token");
        refreshTokens.Verify(repository => repository.Update(existingToken), Times.Once);
        refreshTokens.Verify(repository => repository.AddAsync(It.Is<RefreshToken>(token => token.Token == "refresh-token"), It.IsAny<CancellationToken>()), Times.Once);
    }

    private static Mock<IUnitOfWork> CreateUnitOfWork(
        IGenericRepository<ApplicationUser> users,
        IGenericRepository<RefreshToken> refreshTokens)
    {
        var unitOfWork = new Mock<IUnitOfWork>();
        unitOfWork.SetupGet(work => work.Users).Returns(users);
        unitOfWork.SetupGet(work => work.RefreshTokens).Returns(refreshTokens);
        unitOfWork.Setup(work => work.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        return unitOfWork;
    }

    private static Mock<IJwtTokenService> CreateTokenService()
    {
        var tokenService = new Mock<IJwtTokenService>();
        tokenService.Setup(service => service.GenerateAccessToken(It.IsAny<ApplicationUser>())).Returns("access-token");
        tokenService.Setup(service => service.GenerateRefreshToken()).Returns("refresh-token");
        tokenService.Setup(service => service.GetAccessTokenExpiry()).Returns(DateTime.UtcNow.AddMinutes(15));
        tokenService.Setup(service => service.GetRefreshTokenExpiry()).Returns(DateTime.UtcNow.AddDays(7));

        return tokenService;
    }
}
