using System;
using System.Net;
using System.Reflection;
using System.Threading;
using Polly;

namespace TestPolly.Samples
{
    /// <summary>
    /// Wait and Retry N Times.  Demonstrates behaviour of 'faulting server' we are testing against.
    /// Loops through a series of Http requests, keeping track of each requested
    /// item and reporting server failures when encountering exceptions.
    /// </summary>
    public static class Demo03_WaitAndRetryNTimes_WithEnoughRetries
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
            int eventualFailures = 0;

            // define policy
            var policy = Policy.Handle<Exception>().WaitAndRetry(
                retryCount: 20, // retry 20 times
                sleepDurationProvider: attempt => TimeSpan.FromMilliseconds(200), // wait 200ms between each try
                onRetry: (exception, calculatedWaitDuration) =>
                {
                    // this is the new exception handler
                    ConsoleHelper.WriteLineInColor("Policy loggging: " + exception.Message, ConsoleColor.Yellow);
                    retries++;
                });


            int i = 0;
            // Do the following until a key is pressed
            while (!Console.KeyAvailable)
            {
                i++;

                try
                {
                    // retry according to policy: try 3 times before throwing the exception
                    policy.Execute(() =>
                        {
                            // Make a request and get a response
                            var msg = client.DownloadString(Configuration.WEB_API_ROOT + "/api/values/" + i.ToString());

                            // Display the response message on the console
                            ConsoleHelper.WriteLineInColor("Response : " + msg, ConsoleColor.Green);
                            eventualSuccesses++;
                        });
                }
                catch (Exception e)
                {
                    ConsoleHelper.WriteLineInColor("Request " + i + " eventually failed with: " + e.Message, ConsoleColor.Red);
                    eventualFailures++;
                }

                // Wait half second
                Thread.Sleep(500);
            }

            Console.WriteLine("");
            Console.WriteLine("Total requests made                 : " + i);
            Console.WriteLine("Requests which eventually succeeded : " + eventualSuccesses);
            Console.WriteLine("Retries made to help achieve success: " + retries);
            Console.WriteLine("Requests which eventually failed    : " + eventualFailures);

        }
    }
}
