namespace ProductManagementSystem.Domain.Entities;

/// <summary>
/// Represents an inventory item associated with a product.
/// </summary>
public class Item
{
    /// <summary>
    /// Gets or sets the unique identifier of the item.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the product associated with the item.
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// Gets or sets the quantity available for the item.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or sets the product associated with the item.
    /// </summary>
    public Product? Product { get; set; }
}
