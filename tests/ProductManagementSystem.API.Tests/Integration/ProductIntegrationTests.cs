using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using FluentAssertions;
using ProductManagementSystem.API.Tests.TestInfrastructure;
using ProductManagementSystem.Application.DTOs.Auth;
using ProductManagementSystem.Application.DTOs.Common;
using ProductManagementSystem.Application.DTOs.Products;

namespace ProductManagementSystem.API.Tests.Integration;

public sealed class ProductIntegrationTests(ProductManagementSystemApiFactory factory)
    : IClassFixture<ProductManagementSystemApiFactory>
{
    [Fact]
    public async Task GetProduct_ReturnsProduct_WhenUserIsAuthenticated()
    {
        var client = await CreateAuthenticatedClientAsync("readonly", "User123!");

        var response = await client.GetAsync("/api/v1/products/100");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var body = await response.Content.ReadFromJsonAsync<ApiResponse<ProductDto>>();
        body!.Succeeded.Should().BeTrue();
        body.Data!.ProductName.Should().Be("Seed Product");
    }

    [Fact]
    public async Task CreateProduct_ReturnsCreated_WhenUserIsAdmin()
    {
        var client = await CreateAuthenticatedClientAsync("admin", "Admin123!");

        var response = await client.PostAsJsonAsync("/api/v1/products", new CreateProductDto
        {
            ProductName = "Created Product",
            CreatedBy = "ignored"
        });

        response.StatusCode.Should().Be(HttpStatusCode.Created);
        var body = await response.Content.ReadFromJsonAsync<ApiResponse<ProductDto>>();
        body!.Succeeded.Should().BeTrue();
        body.Data!.ProductName.Should().Be("Created Product");
    }

    [Fact]
    public async Task UpdateProduct_ReturnsOk_WhenUserIsAdmin()
    {
        var client = await CreateAuthenticatedClientAsync("admin", "Admin123!");
        var create = await client.PostAsJsonAsync("/api/v1/products", new CreateProductDto
        {
            ProductName = "Before Update",
            CreatedBy = "ignored"
        });
        var created = await create.Content.ReadFromJsonAsync<ApiResponse<ProductDto>>();

        var response = await client.PutAsJsonAsync($"/api/v1/products/{created!.Data!.Id}", new UpdateProductDto
        {
            ProductName = "After Update",
            ModifiedBy = "ignored"
        });

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var body = await response.Content.ReadFromJsonAsync<ApiResponse<ProductDto>>();
        body!.Succeeded.Should().BeTrue();
        body.Data!.ProductName.Should().Be("After Update");
    }

    [Fact]
    public async Task DeleteProduct_ReturnsOk_WhenUserIsAdmin()
    {
        var client = await CreateAuthenticatedClientAsync("admin", "Admin123!");
        var create = await client.PostAsJsonAsync("/api/v1/products", new CreateProductDto
        {
            ProductName = "Delete Me",
            CreatedBy = "ignored"
        });
        var created = await create.Content.ReadFromJsonAsync<ApiResponse<ProductDto>>();

        var response = await client.DeleteAsync($"/api/v1/products/{created!.Data!.Id}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var getDeleted = await client.GetAsync($"/api/v1/products/{created.Data.Id}");
        getDeleted.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CreateProduct_ReturnsForbidden_WhenUserRoleIsReadOnly()
    {
        var client = await CreateAuthenticatedClientAsync("readonly", "User123!");

        var response = await client.PostAsJsonAsync("/api/v1/products", new CreateProductDto
        {
            ProductName = "Forbidden Product",
            CreatedBy = "readonly"
        });

        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    private async Task<HttpClient> CreateAuthenticatedClientAsync(string userNameOrEmail, string password)
    {
        var client = factory.CreateClient();
        var loginResponse = await client.PostAsJsonAsync("/api/v1/auth/login", new LoginRequestDto
        {
            UserNameOrEmail = userNameOrEmail,
            Password = password
        });
        loginResponse.EnsureSuccessStatusCode();
        var login = await loginResponse.Content.ReadFromJsonAsync<ApiResponse<AuthResponseDto>>();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", login!.Data!.AccessToken);

        return client;
    }
}
