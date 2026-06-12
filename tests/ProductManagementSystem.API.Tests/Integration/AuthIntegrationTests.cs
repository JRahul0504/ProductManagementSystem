using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using ProductManagementSystem.API.Tests.TestInfrastructure;
using ProductManagementSystem.Application.DTOs.Auth;
using ProductManagementSystem.Application.DTOs.Common;

namespace ProductManagementSystem.API.Tests.Integration;

public sealed class AuthIntegrationTests(ProductManagementSystemApiFactory factory)
    : IClassFixture<ProductManagementSystemApiFactory>
{
    [Fact]
    public async Task Login_ReturnsTokens_WhenCredentialsAreValid()
    {
        var client = factory.CreateClient();

        var response = await client.PostAsJsonAsync("/api/v1/auth/login", new LoginRequestDto
        {
            UserNameOrEmail = "admin",
            Password = "Admin123!"
        });

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var body = await response.Content.ReadFromJsonAsync<ApiResponse<AuthResponseDto>>();
        body.Should().NotBeNull();
        body!.Succeeded.Should().BeTrue();
        body.Data!.AccessToken.Should().NotBeNullOrWhiteSpace();
        body.Data.RefreshToken.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task RefreshToken_ReturnsNewTokens_WhenRefreshTokenIsValid()
    {
        var client = factory.CreateClient();
        var login = await client.PostAsJsonAsync("/api/v1/auth/login", new LoginRequestDto
        {
            UserNameOrEmail = "admin",
            Password = "Admin123!"
        });
        var loginBody = await login.Content.ReadFromJsonAsync<ApiResponse<AuthResponseDto>>();

        var response = await client.PostAsJsonAsync("/api/v1/auth/refresh-token", new RefreshTokenRequestDto
        {
            RefreshToken = loginBody!.Data!.RefreshToken
        });

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var body = await response.Content.ReadFromJsonAsync<ApiResponse<AuthResponseDto>>();
        body.Should().NotBeNull();
        body!.Succeeded.Should().BeTrue();
        body.Data!.RefreshToken.Should().NotBe(loginBody.Data.RefreshToken);
    }
}
