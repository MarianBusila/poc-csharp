using System;
using System.Net;
using System.Reflection;
using System.Threading;
using Polly;

namespace TestPolly.Samples
{
    using System.Diagnostics;

    using Polly.CircuitBreaker;

    /// <summary>
    /// Wait and Retry N Times.  Demonstrates behaviour of 'faulting server' we are testing against.
    /// Loops through a series of Http requests, keeping track of each requested
    /// item and reporting server failures when encountering exceptions.
    /// </summary>
    public static class Demo06_WaitAndRetryNestingCircuitBreaker
    {
        public static void Execute()
        {
            Console.WriteLine(MethodBase.GetCurrentMethod().DeclaringType.Name);
            Console.WriteLine("=======");

            // Let's call a web api service to make repeated requests to a server. 
            // The service is programmed to fail after 3 requests in 5 seconds.

            var client = new WebClient();
            int eventualSuccesses = 0;
            int retries = 0;
            int eventualFailuresDueToCircuitBreaking = 0;
            int eventualFailuresForOtherReasons = 0;

            // define wait and retry policy
            var waitAndRetryPolicy = Policy.Handle<Exception>(e => !(e is BrokenCircuitException)) // Exception filtering. We don't retry if the inner circuit-breaker judges the underlying system is out of commission!
                .WaitAndRetryForever(                
                sleepDurationProvider: attempt => TimeSpan.FromMilliseconds(200), // wait 200ms between each try
                onRetry: (exception, calculatedWaitDuration) =>
                {
                    // this is the new exception handler
                    ConsoleHelper.WriteLineInColor("[" + DateTime.Now.ToString("hh:mm:ss.fff") + "]" + ".Log,then retry: " + exception.Message, ConsoleColor.Yellow);
                    retries++;
                });

            // define circuit breaker policy. Break if the action fails 4 times in a row
            var circuitBreakerPolicy = Policy.Handle<Exception>().CircuitBreaker(
                exceptionsAllowedBeforeBreaking: 4,
                durationOfBreak: TimeSpan.FromSeconds(3),
                onBreak: (ex, breakDelay) =>
                    {
                        ConsoleHelper.WriteLineInColor("[" + DateTime.Now.ToString("hh:mm:ss.fff") + "]" + ".Breaker logging: Breaking the circuit for " + breakDelay.TotalMilliseconds + " ms!", ConsoleColor.Magenta);
                        ConsoleHelper.WriteLineInColor("[" + DateTime.Now.ToString("hh:mm:ss") + "]" + "..due to: " + ex.Message, ConsoleColor.Magenta);
                    },
                onReset: () => ConsoleHelper.WriteLineInColor("[" + DateTime.Now.ToString("hh:mm:ss.fff") + "]" + ".Breaker logging: Call ok! Closed the circuit again!", ConsoleColor.Magenta),
                onHalfOpen: () => ConsoleHelper.WriteLineInColor("[" + DateTime.Now.ToString("hh:mm:ss.fff") + "]" + ".Breaker logging: Half-open: Next call is a trial!", ConsoleColor.Magenta)
                );


            int i = 0;
            // Do the following until a key is pressed
            while (!Console.KeyAvailable)
            {
                i++;
                Stopwatch watch = new Stopwatch();
                watch.Start();

                try
                {
                    // retry according to policy: try 3 times 
                    waitAndRetryPolicy.Execute(() =>
                        {
                            // This code is executed within the waitAndRetryPolicy
                            string msg = circuitBreakerPolicy.Execute<String>(() =>
                                {
                                    // Make a request and get a response
                                    return client.DownloadString(Configuration.WEB_API_ROOT + "/api/values/" + i);
                                });

                            watch.Stop();


                            // Display the response message on the console
                            ConsoleHelper.WriteLineInColor("[" + DateTime.Now.ToString("hh:mm:ss.fff") + "]" + "Response : " + msg, ConsoleColor.Green);
                            ConsoleHelper.WriteLineInColor("[" + DateTime.Now.ToString("hh:mm:ss.fff") + "]" + " (after " + watch.ElapsedMilliseconds + "ms)", ConsoleColor.Green);
                            eventualSuccesses++;
                        });
                }
                catch (BrokenCircuitException b)
                {
                    watch.Stop();
                    ConsoleHelper.WriteInColor("[" + DateTime.Now.ToString("hh:mm:ss.fff") + "]" + "Request " + i + " failed with: " + b.GetType().Name, ConsoleColor.Red);
                    ConsoleHelper.WriteLineInColor(" (after " + watch.ElapsedMilliseconds + "ms)", ConsoleColor.Red);
                    eventualFailuresDueToCircuitBreaking++;
                }
                catch (Exception e)
                {
                    watch.Stop();
                    ConsoleHelper.WriteLineInColor("[" + DateTime.Now.ToString("hh:mm:ss.fff") + "]" + "Request " + i + " eventually failed with: " + e.Message, ConsoleColor.Red);
                    ConsoleHelper.WriteLineInColor(" (after " + watch.ElapsedMilliseconds + "ms)", ConsoleColor.Red);
                    eventualFailuresForOtherReasons++;
                }

                // Wait half second
                Thread.Sleep(500);
            }

            Console.WriteLine("");
            Console.WriteLine("Total requests made                 : " + i);
            Console.WriteLine("Requests which eventually succeeded : " + eventualSuccesses);
            Console.WriteLine("Retries made to help achieve success: " + retries);
            Console.WriteLine("Requests failed early by broken circuit : " + eventualFailuresDueToCircuitBreaking);
            Console.WriteLine("Requests which failed after longer delay: " + eventualFailuresForOtherReasons);

        }
    }
}
