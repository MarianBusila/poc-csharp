
namespace TestAsyncAwait
{
    using System;
    using System.Threading.Tasks;

    class Program
    {
        static void Main(string[] args)
        {
            var demo = new AsyncAwaitDemo();
            demo.DoStuff();

            while (true)
            {
                Console.WriteLine("Doing stuff on the main thread ........");
            }
        }
    }

    public class AsyncAwaitDemo
    {
        public async Task DoStuff()
        {
            await Task.Run(() =>
                { LongRunningOperation(); });
        }

        private static async Task<string> LongRunningOperation()
        {
            int counter;

            for (counter = 0; counter < 500000; counter++)
            {
                Console.WriteLine(counter);
            }

            return "Counter = " + counter;
        }
    }
}
