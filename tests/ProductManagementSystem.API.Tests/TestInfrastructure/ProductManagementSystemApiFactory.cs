using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ProductManagementSystem.Domain.Entities;
using ProductManagementSystem.Domain.Enums;
using ProductManagementSystem.Infrastructure.Data;
using ProductManagementSystem.Infrastructure.Security;

namespace ProductManagementSystem.API.Tests.TestInfrastructure;

public sealed class ProductManagementSystemApiFactory : WebApplicationFactory<Program>
{
    private readonly string databaseName = $"ProductManagementSystemTests_{Guid.NewGuid():N}";

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");
        builder.ConfigureServices(services =>
        {
            services.RemoveAll<DbContextOptions<ApplicationDbContext>>();
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseInMemoryDatabase(databaseName);
            });

            using var scope = services.BuildServiceProvider().CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
            SeedDatabase(dbContext);
        });
    }

    private static void SeedDatabase(ApplicationDbContext dbContext)
    {
        var passwordHasher = new PasswordHasher();
        var admin = dbContext.ApplicationUsers.Single(user => user.Id == 1);
        admin.PasswordHash = passwordHasher.HashPassword("Admin123!");
        var user = dbContext.ApplicationUsers.Single(user => user.Id == 2);
        user.PasswordHash = passwordHasher.HashPassword("User123!");

        dbContext.Products.Add(new Product
        {
            Id = 100,
            ProductName = "Seed Product",
            CreatedBy = "admin",
            CreatedOn = DateTime.UtcNow,
            IsDeleted = false
        });

        dbContext.ApplicationUsers.Add(new ApplicationUser
        {
            Id = 100,
            UserName = "readonly",
            Email = "readonly@productmanagement.local",
            PasswordHash = passwordHasher.HashPassword("User123!"),
            Role = UserRole.User,
            IsActive = true,
            CreatedOn = DateTime.UtcNow
        });

        dbContext.SaveChanges();
    }
}
