using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using ProductManagementSystem.Application.DTOs.Products;
using ProductManagementSystem.Application.Interfaces.Services;

namespace ProductManagementSystem.API.Controllers;

/// <summary>
/// Provides product management endpoints.
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Authorize(Roles = "User,Admin")]
[Route("api/v{version:apiVersion}/products")]
public sealed class ProductsController(IProductService productService) : ControllerBase
{
    /// <summary>
    /// Gets a paginated, searchable, and sortable list of products.
    /// </summary>
    /// <param name="request">The paging, search, and sorting request.</param>
    /// <param name="cancellationToken">A token used to cancel the operation.</param>
    /// <returns>The paged product response.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<PagedResponse<ProductDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetProducts(
        [FromQuery] PagedRequestDto request,
        CancellationToken cancellationToken)
    {
        var response = await productService.GetProductsAsync(request, cancellationToken);

        return Ok(response);
    }

    /// <summary>
    /// Gets a product by identifier.
    /// </summary>
    /// <param name="id">The product identifier.</param>
    /// <param name="cancellationToken">A token used to cancel the operation.</param>
    /// <returns>The product response.</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<ProductDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<ProductDto>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetProductById(
        int id,
        CancellationToken cancellationToken)
    {
        var response = await productService.GetProductByIdAsync(id, cancellationToken);

        return response.Succeeded
            ? Ok(response)
            : NotFound(response);
    }

    /// <summary>
    /// Creates a product. Admin role required.
    /// </summary>
    /// <param name="request">The product creation request.</param>
    /// <param name="cancellationToken">A token used to cancel the operation.</param>
    /// <returns>The created product response.</returns>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(ApiResponse<ProductDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<ProductDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> CreateProduct(
        [FromBody] CreateProductDto request,
        CancellationToken cancellationToken)
    {
        request.CreatedBy = User.Identity?.Name ?? request.CreatedBy;
        var response = await productService.CreateProductAsync(request, cancellationToken);

        return response.Succeeded
            ? CreatedAtAction(nameof(GetProductById), new { id = response.Data?.Id, version = "1" }, response)
            : BadRequest(response);
    }

    /// <summary>
    /// Updates a product. Admin role required.
    /// </summary>
    /// <param name="id">The product identifier.</param>
    /// <param name="request">The product update request.</param>
    /// <param name="cancellationToken">A token used to cancel the operation.</param>
    /// <returns>The updated product response.</returns>
    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(ApiResponse<ProductDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<ProductDto>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> UpdateProduct(
        int id,
        [FromBody] UpdateProductDto request,
        CancellationToken cancellationToken)
    {
        request.ModifiedBy = User.Identity?.Name ?? request.ModifiedBy;
        var response = await productService.UpdateProductAsync(id, request, cancellationToken);

        return response.Succeeded
            ? Ok(response)
            : NotFound(response);
    }

    /// <summary>
    /// Soft deletes a product. Admin role required.
    /// </summary>
    /// <param name="id">The product identifier.</param>
    /// <param name="cancellationToken">A token used to cancel the operation.</param>
    /// <returns>The delete operation response.</returns>
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> DeleteProduct(
        int id,
        CancellationToken cancellationToken)
    {
        var response = await productService.DeleteProductAsync(id, cancellationToken);

        return response.Succeeded
            ? Ok(response)
            : NotFound(response);
    }
}
