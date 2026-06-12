namespace ProductManagementSystem.Application.DTOs.Products;

/// <summary>
/// Represents the data required to update a product.
/// </summary>
public sealed class UpdateProductDto
{
    /// <summary>
    /// Gets or sets the product name.
    /// </summary>
    public string ProductName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the user modifying the product.
    /// </summary>
    public string ModifiedBy { get; set; } = string.Empty;
}
