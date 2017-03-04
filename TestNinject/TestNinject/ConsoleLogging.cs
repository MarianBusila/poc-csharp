using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNinject
{
    class ConsoleLogging : ILogging
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
