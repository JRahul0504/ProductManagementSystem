namespace ProductManagementSystem.Application.Interfaces.Security;

/// <summary>
/// Defines password hashing and verification operations.
/// </summary>
public interface IPasswordHasher
{
    /// <summary>
    /// Hashes a plain-text password.
    /// </summary>
    /// <param name="password">The plain-text password.</param>
    /// <returns>The password hash.</returns>
    string HashPassword(string password);

    /// <summary>
    /// Verifies a plain-text password against a stored hash.
    /// </summary>
    /// <param name="password">The plain-text password.</param>
    /// <param name="passwordHash">The stored password hash.</param>
    /// <returns>True when the password is valid; otherwise, false.</returns>
    bool VerifyPassword(string password, string passwordHash);
}
