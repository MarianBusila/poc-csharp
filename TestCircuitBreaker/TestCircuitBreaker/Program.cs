using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace TestCircuitBreaker
{
    class Program
    {
        static void Main(string[] args)
        {
            CircuitBreaker circuitBreaker = new CircuitBreaker(5, 5000);
            ConnectionClient connectionClient = new ConnectionClient();
            for (int i = 0; i < 20; i++)
            {
                Console.Write($"{i} - ");
                try
                {
                    int responseCode;
                    circuitBreaker.Execute(new Action(() => { responseCode = connectionClient.GetListOfItems(i); }));
                    Console.WriteLine("Success");
                }
                catch (CircuitTrippedException ex)
                {
                    Console.WriteLine(typeof(CircuitTrippedException).ToString());
                }
                catch (OpenCircuitException ex)
                {
                    Console.WriteLine(typeof(OpenCircuitException).ToString());
                }
                catch (ArgumentNullException ex)
                {
                    Console.WriteLine(typeof(ArgumentNullException).ToString());
                }
                Thread.Sleep(1000);
            }

            
        }
    }
}
