namespace ProductManagementSystem.Domain.Enums;

/// <summary>
/// Defines the authorization roles available to application users.
/// </summary>
public enum UserRole
{
    /// <summary>
    /// Standard authenticated user with read-focused access.
    /// </summary>
    User = 1,

    /// <summary>
    /// Administrator with elevated access to manage protected resources.
    /// </summary>
    Admin = 2
}
