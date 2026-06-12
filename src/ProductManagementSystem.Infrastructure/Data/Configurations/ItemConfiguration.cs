namespace ProductManagementSystem.Infrastructure.Data.Configurations;

/// <summary>
/// Configures the Item entity persistence model.
/// </summary>
public sealed class ItemConfiguration : IEntityTypeConfiguration<Item>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.ToTable("Item", table =>
        {
            table.HasCheckConstraint("CK_Item_Quantity_Positive", "[Quantity] > 0");
        });

        builder.HasKey(item => item.Id);

        builder.Property(item => item.Id)
            .ValueGeneratedOnAdd();

        builder.Property(item => item.ProductId)
            .IsRequired();

        builder.Property(item => item.Quantity)
            .IsRequired();

        builder.HasIndex(item => item.ProductId)
            .HasDatabaseName("IX_Item_ProductId");
    }
}
