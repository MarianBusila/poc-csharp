using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCore31WebApi
{
    public class ExampleHealthCheck : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var healthCheckResultHealthy = true;
            
            if(healthCheckResultHealthy)
            {
                return Task.FromResult(HealthCheckResult.Healthy("A healthy result.", new Dictionary<string, object> { { "key", "value" } }));
            }

            return Task.FromResult(HealthCheckResult.Unhealthy("Un unhealthy result.", new Exception("Status check failed because of an exception")));
        }
    }
}
