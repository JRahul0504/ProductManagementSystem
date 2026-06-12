using Microsoft.Extensions.Diagnostics.HealthChecks;
using ProductManagementSystem.Infrastructure.Data;

namespace ProductManagementSystem.API.HealthChecks;

/// <summary>
/// Verifies database connectivity for readiness checks.
/// </summary>
public sealed class DatabaseHealthCheck(ApplicationDbContext dbContext) : IHealthCheck
{
    /// <inheritdoc />
    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        var canConnect = await dbContext.Database.CanConnectAsync(cancellationToken);

        return canConnect
            ? HealthCheckResult.Healthy("Database connection is healthy.")
            : HealthCheckResult.Unhealthy("Database connection is unavailable.");
    }
}
