using ProductManagementSystem.Application.DTOs.Products;

namespace ProductManagementSystem.Application.Interfaces.Services;

/// <summary>
/// Defines application operations for product management.
/// </summary>
public interface IProductService
{
    /// <summary>
    /// Gets a paginated list of products.
    /// </summary>
    /// <param name="request">The pagination request.</param>
    /// <param name="cancellationToken">A token used to cancel the operation.</param>
    /// <returns>A paginated product response.</returns>
    Task<ApiResponse<PagedResponse<ProductDto>>> GetProductsAsync(
        PagedRequestDto request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a product by identifier.
    /// </summary>
    /// <param name="id">The product identifier.</param>
    /// <param name="cancellationToken">A token used to cancel the operation.</param>
    /// <returns>The product response.</returns>
    Task<ApiResponse<ProductDto>> GetProductByIdAsync(
        int id,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a product.
    /// </summary>
    /// <param name="request">The product creation request.</param>
    /// <param name="cancellationToken">A token used to cancel the operation.</param>
    /// <returns>The created product response.</returns>
    Task<ApiResponse<ProductDto>> CreateProductAsync(
        CreateProductDto request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates a product.
    /// </summary>
    /// <param name="id">The product identifier.</param>
    /// <param name="request">The product update request.</param>
    /// <param name="cancellationToken">A token used to cancel the operation.</param>
    /// <returns>The updated product response.</returns>
    Task<ApiResponse<ProductDto>> UpdateProductAsync(
        int id,
        UpdateProductDto request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a product.
    /// </summary>
    /// <param name="id">The product identifier.</param>
    /// <param name="cancellationToken">A token used to cancel the operation.</param>
    /// <returns>The operation response.</returns>
    Task<ApiResponse<bool>> DeleteProductAsync(
        int id,
        CancellationToken cancellationToken = default);
}
