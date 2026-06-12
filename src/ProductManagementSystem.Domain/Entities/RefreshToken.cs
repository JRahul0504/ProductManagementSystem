namespace ProductManagementSystem.Domain.Entities;

/// <summary>
/// Represents a refresh token issued to an authenticated application user.
/// </summary>
public class RefreshToken
{
    /// <summary>
    /// Gets or sets the unique identifier of the refresh token record.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the application user that owns the token.
    /// </summary>
    public int ApplicationUserId { get; set; }

    /// <summary>
    /// Gets or sets the stored refresh token value or token hash.
    /// </summary>
    public string Token { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the UTC date and time when the refresh token expires.
    /// </summary>
    public DateTime ExpiresOn { get; set; }

    /// <summary>
    /// Gets or sets the UTC date and time when the refresh token was created.
    /// </summary>
    public DateTime CreatedOn { get; set; }

    /// <summary>
    /// Gets or sets the user name, identifier, or client metadata that created the token.
    /// </summary>
    public string CreatedBy { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the UTC date and time when the refresh token was revoked.
    /// </summary>
    public DateTime? RevokedOn { get; set; }

    /// <summary>
    /// Gets or sets the user name, identifier, or client metadata that revoked the token.
    /// </summary>
    public string? RevokedBy { get; set; }

    /// <summary>
    /// Gets or sets the replacement refresh token value or hash created during token rotation.
    /// </summary>
    public string? ReplacedByToken { get; set; }

    /// <summary>
    /// Gets or sets the reason the refresh token was revoked.
    /// </summary>
    public string? RevocationReason { get; set; }

    /// <summary>
    /// Gets a value indicating whether the refresh token has expired.
    /// </summary>
    public bool IsExpired => DateTime.UtcNow >= ExpiresOn;

    /// <summary>
    /// Gets a value indicating whether the refresh token has been revoked.
    /// </summary>
    public bool IsRevoked => RevokedOn.HasValue;

    /// <summary>
    /// Gets a value indicating whether the refresh token can still be used.
    /// </summary>
    public bool IsActive => !IsRevoked && !IsExpired;

    /// <summary>
    /// Gets or sets the application user that owns the token.
    /// </summary>
    public ApplicationUser? ApplicationUser { get; set; }
}
