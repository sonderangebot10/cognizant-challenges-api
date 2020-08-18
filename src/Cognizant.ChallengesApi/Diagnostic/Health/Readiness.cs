using Cognizant.Infrastructure.Data.PgSql.Challenges.Repositories;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cognizant.ChallangesApi.Diagnostic.Health
{
    public class Readiness : IHostedService, IDisposable
    {
        private readonly ILogger _logger;
        private readonly StartupHostedServiceHealthCheck _startupHostedServiceHealthCheck;
        private readonly IChallengesRepository _repo;

        public Readiness(ILogger<Readiness> logger,
            StartupHostedServiceHealthCheck startupHostedServiceHealthCheck,
            IChallengesRepository repo)
        {
            _logger = logger;
            _startupHostedServiceHealthCheck = startupHostedServiceHealthCheck;
            _repo = repo;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("startup hosted service is starting");


            Task.Run(async () =>
            {
                var result = await _repo.GetChallengesAsync();

                if(result.Count() == 0)
                {
                    _logger.LogError("application does not contain any challanges");
                    return;
                }

                _startupHostedServiceHealthCheck.StartupTaskCompleted = true;

                _logger.LogInformation("startup hosted services started");
            }, cancellationToken);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("startup hosted services is stopping.");

            return Task.CompletedTask;
        }

        public void Dispose()
        {
        }
    }
}
