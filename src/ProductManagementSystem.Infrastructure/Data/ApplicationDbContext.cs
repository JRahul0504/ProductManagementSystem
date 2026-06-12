namespace ProductManagementSystem.Infrastructure.Data;

/// <summary>
/// Represents the Entity Framework Core database context for the application.
/// </summary>
/// <param name="options">The database context options.</param>
public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : DbContext(options)
{
    /// <summary>
    /// Gets or sets the products table.
    /// </summary>
    public DbSet<Product> Products => Set<Product>();

    /// <summary>
    /// Gets or sets the items table.
    /// </summary>
    public DbSet<Item> Items => Set<Item>();

    /// <summary>
    /// Gets or sets the application users table.
    /// </summary>
    public DbSet<ApplicationUser> ApplicationUsers => Set<ApplicationUser>();

    /// <summary>
    /// Gets or sets the refresh tokens table.
    /// </summary>
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
