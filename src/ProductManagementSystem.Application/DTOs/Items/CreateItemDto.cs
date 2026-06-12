namespace ProductManagementSystem.Application.DTOs.Items;

/// <summary>
/// Represents the data required to create an item.
/// </summary>
public sealed class CreateItemDto
{
    /// <summary>
    /// Gets or sets the associated product identifier.
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// Gets or sets the item quantity.
    /// </summary>
    public int Quantity { get; set; }
}
