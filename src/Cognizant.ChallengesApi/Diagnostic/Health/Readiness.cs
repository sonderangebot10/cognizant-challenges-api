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

        public Readiness(ILogger<Readiness> logger,
            StartupHostedServiceHealthCheck startupHostedServiceHealthCheck)
        {
            _logger = logger;
            _startupHostedServiceHealthCheck = startupHostedServiceHealthCheck;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("startup hosted service is starting");


            Task.Run(async () =>
            {
                //CHECK IF REPOSITORY IS RESPONDING - LET'S HOPE I WON'T FORGET THIS

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
