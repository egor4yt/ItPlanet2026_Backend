using System.Reflection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Launchpad.Api.HealthChecks;

/// <summary>
///     HealthCheck
/// </summary>
public class ApiHealthCheck : IHealthCheck
{
    /// <summary>
    ///     Async health-check
    /// </summary>
    /// <param name="context">Health check context</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Health check result</returns>
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var versionNumber = assembly.GetName().Version;

        return Task.FromResult(HealthCheckResult.Healthy($"Build {versionNumber}"));
    }
}