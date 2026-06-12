using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using ProductManagementSystem.Application.DTOs.Items;
using ProductManagementSystem.Application.Interfaces.Services;

namespace ProductManagementSystem.API.Controllers;

/// <summary>
/// Provides item management endpoints.
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Authorize(Roles = "User,Admin")]
[Route("api/v{version:apiVersion}")]
public sealed class ItemsController(IItemService itemService) : ControllerBase
{
    /// <summary>
    /// Gets items for a product.
    /// </summary>
    /// <param name="productId">The product identifier.</param>
    /// <param name="cancellationToken">A token used to cancel the operation.</param>
    /// <returns>The item list response.</returns>
    [HttpGet("products/{productId:int}/items")]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<ItemDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<ItemDto>>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetItemsByProductId(
        int productId,
        CancellationToken cancellationToken)
    {
        var response = await itemService.GetItemsByProductIdAsync(productId, cancellationToken);

        return response.Succeeded
            ? Ok(response)
            : NotFound(response);
    }

    /// <summary>
    /// Gets an item by identifier.
    /// </summary>
    /// <param name="id">The item identifier.</param>
    /// <param name="cancellationToken">A token used to cancel the operation.</param>
    /// <returns>The item response.</returns>
    [HttpGet("items/{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<ItemDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<ItemDto>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetItemById(
        int id,
        CancellationToken cancellationToken)
    {
        var response = await itemService.GetItemByIdAsync(id, cancellationToken);

        return response.Succeeded
            ? Ok(response)
            : NotFound(response);
    }

    /// <summary>
    /// Creates an item. Admin role required.
    /// </summary>
    /// <param name="request">The item creation request.</param>
    /// <param name="cancellationToken">A token used to cancel the operation.</param>
    /// <returns>The created item response.</returns>
    [HttpPost("items")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(ApiResponse<ItemDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<ItemDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> CreateItem(
        [FromBody] CreateItemDto request,
        CancellationToken cancellationToken)
    {
        var response = await itemService.CreateItemAsync(request, cancellationToken);

        return response.Succeeded
            ? CreatedAtAction(nameof(GetItemById), new { id = response.Data?.Id, version = "1" }, response)
            : BadRequest(response);
    }

    /// <summary>
    /// Updates an item. Admin role required.
    /// </summary>
    /// <param name="id">The item identifier.</param>
    /// <param name="request">The item update request.</param>
    /// <param name="cancellationToken">A token used to cancel the operation.</param>
    /// <returns>The updated item response.</returns>
    [HttpPut("items/{id:int}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(ApiResponse<ItemDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<ItemDto>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> UpdateItem(
        int id,
        [FromBody] UpdateItemDto request,
        CancellationToken cancellationToken)
    {
        var response = await itemService.UpdateItemAsync(id, request, cancellationToken);

        return response.Succeeded
            ? Ok(response)
            : NotFound(response);
    }

    /// <summary>
    /// Deletes an item. Admin role required.
    /// </summary>
    /// <param name="id">The item identifier.</param>
    /// <param name="cancellationToken">A token used to cancel the operation.</param>
    /// <returns>The delete operation response.</returns>
    [HttpDelete("items/{id:int}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> DeleteItem(
        int id,
        CancellationToken cancellationToken)
    {
        var response = await itemService.DeleteItemAsync(id, cancellationToken);

        return response.Succeeded
            ? Ok(response)
            : NotFound(response);
    }
}
