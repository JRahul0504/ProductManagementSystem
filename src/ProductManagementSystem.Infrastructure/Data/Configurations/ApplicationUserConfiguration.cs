using ProductManagementSystem.Infrastructure.Data.Seed;

namespace ProductManagementSystem.Infrastructure.Data.Configurations;

/// <summary>
/// Configures the ApplicationUser entity persistence model.
/// </summary>
public sealed class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.ToTable("ApplicationUser");

        builder.HasKey(user => user.Id);

        builder.Property(user => user.Id)
            .ValueGeneratedOnAdd();

        builder.Property(user => user.UserName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(user => user.Email)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(user => user.PasswordHash)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(user => user.Role)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(user => user.IsActive)
            .IsRequired();

        builder.Property(user => user.CreatedOn)
            .IsRequired();

        builder.HasMany(user => user.RefreshTokens)
            .WithOne(refreshToken => refreshToken.ApplicationUser)
            .HasForeignKey(refreshToken => refreshToken.ApplicationUserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(user => user.UserName)
            .IsUnique()
            .HasDatabaseName("UX_ApplicationUser_UserName");

        builder.HasIndex(user => user.Email)
            .IsUnique()
            .HasDatabaseName("UX_ApplicationUser_Email");

        builder.HasData(
            ApplicationUserSeed.AdminUser,
            ApplicationUserSeed.NormalUser);
    }
}
