using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Threading;
using System.Threading.Tasks;

namespace Cognizant.ChallangesApi.Diagnostic.Health
{
    public class StartupHostedServiceHealthCheck : IHealthCheck
    {
        private static bool _startupTaskCompleted;

        public string Name => "slow_dependency_check";

        public bool StartupTaskCompleted
        {
            get => _startupTaskCompleted;
            set => _startupTaskCompleted = value;
        }

        public Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = default)
        {
            if (StartupTaskCompleted)
            {
                return Task.FromResult(
                    HealthCheckResult.Healthy("GOOD"));
            }

            return Task.FromResult(
                HealthCheckResult.Unhealthy("The startup task is still running."));
        }
    }
}