using ProductManagementSystem.Infrastructure.Data.Repositories;

namespace ProductManagementSystem.Infrastructure.Data;

/// <summary>
/// Coordinates repository access and persistence commits for the application database.
/// </summary>
public sealed class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext dbContext;
    private readonly Dictionary<Type, object> repositories = [];

    /// <summary>
    /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
    /// </summary>
    /// <param name="dbContext">The application database context.</param>
    public UnitOfWork(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
        Products = new GenericRepository<Product>(dbContext);
        Items = new GenericRepository<Item>(dbContext);
        Users = new GenericRepository<ApplicationUser>(dbContext);
        RefreshTokens = new GenericRepository<RefreshToken>(dbContext);
    }

    /// <inheritdoc />
    public IGenericRepository<Product> Products { get; }

    /// <inheritdoc />
    public IGenericRepository<Item> Items { get; }

    /// <inheritdoc />
    public IGenericRepository<ApplicationUser> Users { get; }

    /// <inheritdoc />
    public IGenericRepository<RefreshToken> RefreshTokens { get; }

    /// <inheritdoc />
    public IGenericRepository<T> Repository<T>()
        where T : class
    {
        var entityType = typeof(T);

        if (repositories.TryGetValue(entityType, out var repository))
        {
            return (IGenericRepository<T>)repository;
        }

        var newRepository = new GenericRepository<T>(dbContext);
        repositories[entityType] = newRepository;

        return newRepository;
    }

    /// <inheritdoc />
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.SaveChangesAsync(cancellationToken);
    }
}
