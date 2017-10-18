using System;
using System.Net;
using System.Reflection;
using System.Threading;
using Polly;

namespace TestPolly.Samples
{
    using System.Diagnostics;

    using Polly.CircuitBreaker;
    using Polly.Fallback;
    using Polly.Wrap;

    /// <summary>
    /// /// Demonstrates a PolicyWrap including two Fallback policies (for different exceptions), WaitAndRetry and CircuitBreaker.
    /// Loops through a series of Http requests, keeping track of each requested
    /// item and reporting server failures when encountering exceptions.
    /// </summary>
    public static class Demo08_Wrap_Fallback_WaitAndRetry_CircuitBreaker
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
            Stopwatch watch = null;

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

            // define a fallback policy: provide a nice substitute message to the user, if we found the circuit was broken.
            FallbackPolicy<String> fallbackForCircuitBreaker = Policy<String>.Handle<BrokenCircuitException>().Fallback(
                fallbackValue:/* Demonstrates fallback value syntax */ "Please try again later [Fallback for broken circuit]",
                onFallback: b =>
                    {
                        watch.Stop();
                        ConsoleHelper.WriteInColor("[" + DateTime.Now.ToString("hh:mm:ss.fff") + "]" + "Fallback catches failed with: " + b.Exception.Message, ConsoleColor.Red);
                        ConsoleHelper.WriteLineInColor(" (after " + watch.ElapsedMilliseconds + "ms)", ConsoleColor.Red);
                        eventualFailuresDueToCircuitBreaking++;
                    });

            // Define a fallback policy: provide a substitute string to the user, for any exception.
            FallbackPolicy<String> fallbackForAnyException = Policy<String>
                .Handle<Exception>()
                .Fallback(
                    fallbackAction: /* Demonstrates fallback action/func syntax */ () => { return "Please try again later [Fallback for any exception]"; },
                    onFallback: e =>
                    {
                        watch.Stop();
                        ConsoleHelper.WriteInColor("[" + DateTime.Now.ToString("hh:mm:ss.fff") + "]" + "Fallback catches eventually failed with: " + e.Exception.Message, ConsoleColor.Red);
                        ConsoleHelper.WriteLineInColor(" (after " + watch.ElapsedMilliseconds + "ms)", ConsoleColor.Red);
                        eventualFailuresForOtherReasons++;
                    }
                );

            // As demo07: we combine the waitAndRetryPolicy and circuitBreakerPolicy into a PolicyWrap, using the *static* Policy.Wrap syntax.
            PolicyWrap myResilienceStrategy = Policy.Wrap(waitAndRetryPolicy, circuitBreakerPolicy);

            // Added in demo08: we wrap the two fallback policies onto the front of the existing wrap too.  Demonstrates the *instance* wrap syntax. And the fact that the PolicyWrap myResilienceStrategy from above is just another Policy, which can be onward-wrapped too.  
            // With this pattern, you can build an overall resilience strategy programmatically, reusing some common parts (eg PolicyWrap myResilienceStrategy) but varying other parts (eg Fallback) individually for different calls.
            PolicyWrap<String> policyWrap = fallbackForAnyException.Wrap(fallbackForCircuitBreaker.Wrap(myResilienceStrategy));
            // For info: Equivalent to: PolicyWrap<String> policyWrap = Policy.Wrap(fallbackForAnyException, fallbackForCircuitBreaker, waitAndRetryPolicy, circuitBreakerPolicy);

            int i = 0;
            // Do the following until a key is pressed
            while (!Console.KeyAvailable)
            {
                i++;
                watch = new Stopwatch();
                watch.Start();

                try
                {
                    // Retry the following call according to the policy wrap
                    string msg = policyWrap.Execute(() =>
                        {
                            return client.DownloadString(Configuration.WEB_API_ROOT + "/api/values/" + i);                             
                        });
                    watch.Stop();


                    // Display the response message on the console
                    ConsoleHelper.WriteLineInColor("[" + DateTime.Now.ToString("hh:mm:ss.fff") + "]" + "Response : " + msg, ConsoleColor.Green);
                    ConsoleHelper.WriteLineInColor("[" + DateTime.Now.ToString("hh:mm:ss.fff") + "]" + " (after " + watch.ElapsedMilliseconds + "ms)", ConsoleColor.Green);
                    eventualSuccesses++;
                }
                catch (Exception e) // try-catch not needed, now that we have a Fallback.Handle<Exception>.  It's only been left in to *demonstrate* it should never get hit.
                {
                    throw new InvalidOperationException("Should never arrive here.  Use of fallbackForAnyException should have provided nice fallback value for any exceptions.", e);
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
