namespace ProductManagementSystem.API.Configuration;

/// <summary>
/// Represents HTTP security header configuration.
/// </summary>
public sealed class SecurityHeadersOptions
{
    /// <summary>
    /// Gets the configuration section name.
    /// </summary>
    public const string SectionName = "SecurityHeaders";

    /// <summary>
    /// Gets or sets the Content-Security-Policy header value.
    /// </summary>
    public string ContentSecurityPolicy { get; init; } = "default-src 'self'; frame-ancestors 'none';";

    /// <summary>
    /// Gets or sets the Referrer-Policy header value.
    /// </summary>
    public string ReferrerPolicy { get; init; } = "no-referrer";

    /// <summary>
    /// Gets or sets the Permissions-Policy header value.
    /// </summary>
    public string PermissionsPolicy { get; init; } = "geolocation=(), microphone=(), camera=()";
}
