namespace ProductManagementSystem.Infrastructure.Data.Configurations;

/// <summary>
/// Configures the RefreshToken entity persistence model.
/// </summary>
public sealed class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable("RefreshToken");

        builder.HasKey(refreshToken => refreshToken.Id);

        builder.Property(refreshToken => refreshToken.Id)
            .ValueGeneratedOnAdd();

        builder.Property(refreshToken => refreshToken.ApplicationUserId)
            .IsRequired();

        builder.Property(refreshToken => refreshToken.Token)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(refreshToken => refreshToken.ExpiresOn)
            .IsRequired();

        builder.Property(refreshToken => refreshToken.CreatedOn)
            .IsRequired();

        builder.Property(refreshToken => refreshToken.CreatedBy)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(refreshToken => refreshToken.RevokedBy)
            .HasMaxLength(100);

        builder.Property(refreshToken => refreshToken.ReplacedByToken)
            .HasMaxLength(500);

        builder.Property(refreshToken => refreshToken.RevocationReason)
            .HasMaxLength(255);

        builder.Ignore(refreshToken => refreshToken.IsExpired);
        builder.Ignore(refreshToken => refreshToken.IsRevoked);
        builder.Ignore(refreshToken => refreshToken.IsActive);

        builder.HasIndex(refreshToken => refreshToken.Token)
            .IsUnique()
            .HasDatabaseName("UX_RefreshToken_Token");

        builder.HasIndex(refreshToken => refreshToken.ApplicationUserId)
            .HasDatabaseName("IX_RefreshToken_ApplicationUserId");
    }
}
