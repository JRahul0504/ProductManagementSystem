using ProductManagementSystem.Domain.Enums;

namespace ProductManagementSystem.Infrastructure.Data.Seed;

/// <summary>
/// Provides deterministic seed users for local development and assessment review.
/// </summary>
internal static class ApplicationUserSeed
{
    /// <summary>
    /// Gets the seeded administrator user.
    /// </summary>
    public static ApplicationUser AdminUser => new()
    {
        Id = 1,
        UserName = "admin",
        Email = "admin@productmanagement.local",
        PasswordHash = "100000.yOHGuP3qEjim04QN9urmuQ==.99x1NIQvRhE3r1c1/ZyHaIyRWlbvxn4URtad4UmFjw4=",
        Role = UserRole.Admin,
        IsActive = true,
        CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
    };

    /// <summary>
    /// Gets the seeded normal user.
    /// </summary>
    public static ApplicationUser NormalUser => new()
    {
        Id = 2,
        UserName = "user",
        Email = "user@productmanagement.local",
        PasswordHash = "100000.k9lK4pf75zheC7l4wfvxLA==.kiHUWnM91n1qUwzWDkdrLP7yD2ko9oZrdPZjUg7RH5Y=",
        Role = UserRole.User,
        IsActive = true,
        CreatedOn = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
    };
}
