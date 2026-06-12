namespace ProductManagementSystem.API.Configuration;

/// <summary>
/// Represents API rate limiting configuration.
/// </summary>
public sealed class RateLimitingOptions
{
    /// <summary>
    /// Gets the configuration section name.
    /// </summary>
    public const string SectionName = "RateLimiting";

    /// <summary>
    /// Gets or sets the maximum number of permitted requests in a window.
    /// </summary>
    public int PermitLimit { get; init; } = 100;

    /// <summary>
    /// Gets or sets the fixed window duration in seconds.
    /// </summary>
    public int WindowSeconds { get; init; } = 60;

    /// <summary>
    /// Gets or sets the number of requests that can wait in the queue.
    /// </summary>
    public int QueueLimit { get; init; }
}
