namespace ProductManagementSystem.Application.DTOs.Products;

/// <summary>
/// Represents the data required to create a product.
/// </summary>
public sealed class CreateProductDto
{
    /// <summary>
    /// Gets or sets the product name.
    /// </summary>
    public string ProductName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the user creating the product.
    /// </summary>
    public string CreatedBy { get; set; } = string.Empty;
}
