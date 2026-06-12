namespace ProductManagementSystem.Application.DTOs.Items;

/// <summary>
/// Represents item data returned to API clients.
/// </summary>
public sealed class ItemDto
{
    /// <summary>
    /// Gets or sets the item identifier.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the associated product identifier.
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// Gets or sets the item quantity.
    /// </summary>
    public int Quantity { get; set; }
}
