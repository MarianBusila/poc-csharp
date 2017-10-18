using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPolly
{
    class Program
    {
        static void Main(string[] args)
        {
            // Synchronous samples

            // Samples.Demo00_NoPolicy.Execute();
            // Samples.Demo01_RetryNTimes.Execute();
            // Samples.Demo02_WaitAndRetryNTimes.Execute();
            // Samples.Demo03_WaitAndRetryNTimes_WithEnoughRetries.Execute();
            // Samples.Demo04_WaitAndRetryForever.Execute();
            // Samples.Demo05_WaitAndRetryNTimes_WithExponentialBackoff.Execute();
            // Samples.Demo06_WaitAndRetryNestingCircuitBreaker.Execute();
            Samples.Demo07_WaitAndRetryNestingCircuitBreakerUsingPolicyWrap.Execute();

        }
    }
}
