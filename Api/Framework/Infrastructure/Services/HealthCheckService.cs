using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace TerraMours.Framework.Infrastructure.Services
{
    public class HealthCheckService : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var isHealthy = true;

            //todo 逻辑

            if (isHealthy)
            {
                return Task.FromResult(
                    HealthCheckResult.Healthy("健康可视化"));
            }

            return Task.FromResult(
                new HealthCheckResult(
                    context.Registration.FailureStatus, "不健康可视化"));
        }
    }
}
