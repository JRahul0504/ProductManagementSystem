using ProductManagementSystem.Domain.Enums;

namespace ProductManagementSystem.Domain.Entities;

/// <summary>
/// Represents an application user that can authenticate with the API.
/// </summary>
public class ApplicationUser
{
    /// <summary>
    /// Gets or sets the unique identifier of the application user.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the unique user name used for authentication and audit tracking.
    /// </summary>
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the email address of the application user.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the hashed password for the application user.
    /// </summary>
    public string PasswordHash { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the role assigned to the application user.
    /// </summary>
    public UserRole Role { get; set; } = UserRole.User;

    /// <summary>
    /// Gets or sets a value indicating whether the user account is active.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Gets or sets the UTC date and time when the user account was created.
    /// </summary>
    public DateTime CreatedOn { get; set; }

    /// <summary>
    /// Gets or sets the refresh tokens issued to the application user.
    /// </summary>
    public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
}
