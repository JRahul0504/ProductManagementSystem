namespace ProductManagementSystem.Application.DTOs.Items;

/// <summary>
/// Represents the data required to update an item.
/// </summary>
public sealed class UpdateItemDto
{
    /// <summary>
    /// Gets or sets the item quantity.
    /// </summary>
    public int Quantity { get; set; }
}
