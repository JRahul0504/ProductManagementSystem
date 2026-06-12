using ProductManagementSystem.Application.DTOs.Items;

namespace ProductManagementSystem.Application.Interfaces.Services;

/// <summary>
/// Defines application operations for item management.
/// </summary>
public interface IItemService
{
    /// <summary>
    /// Gets items for a product.
    /// </summary>
    /// <param name="productId">The product identifier.</param>
    /// <param name="cancellationToken">A token used to cancel the operation.</param>
    /// <returns>The product item response.</returns>
    Task<ApiResponse<IReadOnlyList<ItemDto>>> GetItemsByProductIdAsync(
        int productId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets an item by identifier.
    /// </summary>
    /// <param name="id">The item identifier.</param>
    /// <param name="cancellationToken">A token used to cancel the operation.</param>
    /// <returns>The item response.</returns>
    Task<ApiResponse<ItemDto>> GetItemByIdAsync(
        int id,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates an item.
    /// </summary>
    /// <param name="request">The item creation request.</param>
    /// <param name="cancellationToken">A token used to cancel the operation.</param>
    /// <returns>The created item response.</returns>
    Task<ApiResponse<ItemDto>> CreateItemAsync(
        CreateItemDto request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an item.
    /// </summary>
    /// <param name="id">The item identifier.</param>
    /// <param name="request">The item update request.</param>
    /// <param name="cancellationToken">A token used to cancel the operation.</param>
    /// <returns>The updated item response.</returns>
    Task<ApiResponse<ItemDto>> UpdateItemAsync(
        int id,
        UpdateItemDto request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes an item.
    /// </summary>
    /// <param name="id">The item identifier.</param>
    /// <param name="cancellationToken">A token used to cancel the operation.</param>
    /// <returns>The operation response.</returns>
    Task<ApiResponse<bool>> DeleteItemAsync(
        int id,
        CancellationToken cancellationToken = default);
}
