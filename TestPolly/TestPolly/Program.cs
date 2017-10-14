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

            Samples.Demo00_NoPolicy.Execute();

        }
    }
}
