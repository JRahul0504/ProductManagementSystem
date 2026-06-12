namespace ProductManagementSystem.Application.Interfaces.Persistence;

/// <summary>
/// Coordinates repository access and persistence commits.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Gets the product repository.
    /// </summary>
    IGenericRepository<Product> Products { get; }

    /// <summary>
    /// Gets the item repository.
    /// </summary>
    IGenericRepository<Item> Items { get; }

    /// <summary>
    /// Gets the application user repository.
    /// </summary>
    IGenericRepository<ApplicationUser> Users { get; }

    /// <summary>
    /// Gets the refresh token repository.
    /// </summary>
    IGenericRepository<RefreshToken> RefreshTokens { get; }

    /// <summary>
    /// Gets a repository for the specified entity type.
    /// </summary>
    /// <typeparam name="T">The entity type.</typeparam>
    /// <returns>A generic repository instance.</returns>
    IGenericRepository<T> Repository<T>()
        where T : class;

    /// <summary>
    /// Commits pending persistence changes.
    /// </summary>
    /// <param name="cancellationToken">A token used to cancel the operation.</param>
    /// <returns>The number of affected records.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
