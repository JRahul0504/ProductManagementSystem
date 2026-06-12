namespace ProductManagementSystem.API.Configuration;

public sealed class CorsOptions
{
    public const string SectionName = "Cors";

    public string PolicyName { get; init; } = "DefaultCorsPolicy";

    public string[] AllowedOrigins { get; init; } = [];
}
