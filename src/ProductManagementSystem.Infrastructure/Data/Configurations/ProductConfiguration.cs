namespace ProductManagementSystem.Infrastructure.Data.Configurations;

/// <summary>
/// Configures the Product entity persistence model.
/// </summary>
public sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Product");

        builder.HasKey(product => product.Id);

        builder.Property(product => product.Id)
            .ValueGeneratedOnAdd();

        builder.Property(product => product.ProductName)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(product => product.CreatedBy)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(product => product.CreatedOn)
            .IsRequired();

        builder.Property(product => product.ModifiedBy)
            .HasMaxLength(100);

        builder.Property(product => product.IsDeleted)
            .IsRequired();

        builder.Property(product => product.DeletedBy)
            .HasMaxLength(100);

        builder.HasMany(product => product.Items)
            .WithOne(item => item.Product)
            .HasForeignKey(item => item.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(product => product.ProductName)
            .HasDatabaseName("IX_Product_ProductName");

        builder.HasIndex(product => product.IsDeleted)
            .HasDatabaseName("IX_Product_IsDeleted");
    }
}
