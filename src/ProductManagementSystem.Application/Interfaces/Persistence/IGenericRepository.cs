namespace ProductManagementSystem.Application.Interfaces.Persistence;

/// <summary>
/// Defines common persistence operations for an aggregate or entity.
/// </summary>
/// <typeparam name="T">The entity type.</typeparam>
public interface IGenericRepository<T>
    where T : class
{
    /// <summary>
    /// Gets an entity by its integer identifier.
    /// </summary>
    /// <param name="id">The entity identifier.</param>
    /// <param name="cancellationToken">A token used to cancel the operation.</param>
    /// <returns>The entity when found; otherwise, null.</returns>
    Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all entities.
    /// </summary>
    /// <param name="cancellationToken">A token used to cancel the operation.</param>
    /// <returns>The matching entities.</returns>
    Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a paginated list of entities.
    /// </summary>
    /// <param name="pageNumber">The requested page number.</param>
    /// <param name="pageSize">The requested page size.</param>
    /// <param name="cancellationToken">A token used to cancel the operation.</param>
    /// <returns>The matching page of entities.</returns>
    Task<IReadOnlyList<T>> GetPagedAsync(
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Finds entities that match the supplied predicate.
    /// </summary>
    /// <param name="predicate">The filter predicate.</param>
    /// <param name="cancellationToken">A token used to cancel the operation.</param>
    /// <returns>The matching entities.</returns>
    Task<IReadOnlyList<T>> FindAsync(
        Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the first entity that matches the supplied predicate.
    /// </summary>
    /// <param name="predicate">The filter predicate.</param>
    /// <param name="cancellationToken">A token used to cancel the operation.</param>
    /// <returns>The first matching entity when found; otherwise, null.</returns>
    Task<T?> FirstOrDefaultAsync(
        Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Determines whether any entity matches the supplied predicate.
    /// </summary>
    /// <param name="predicate">The filter predicate.</param>
    /// <param name="cancellationToken">A token used to cancel the operation.</param>
    /// <returns>True when a matching entity exists; otherwise, false.</returns>
    Task<bool> AnyAsync(
        Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Counts all entities.
    /// </summary>
    /// <param name="cancellationToken">A token used to cancel the operation.</param>
    /// <returns>The total entity count.</returns>
    Task<int> CountAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Counts entities that match the supplied predicate.
    /// </summary>
    /// <param name="predicate">The filter predicate.</param>
    /// <param name="cancellationToken">A token used to cancel the operation.</param>
    /// <returns>The matching entity count.</returns>
    Task<int> CountAsync(
        Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds a new entity.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    /// <param name="cancellationToken">A token used to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task AddAsync(T entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Marks an entity as modified.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    void Update(T entity);

    /// <summary>
    /// Marks an entity as deleted.
    /// </summary>
    /// <param name="entity">The entity to delete.</param>
    void Delete(T entity);
}
