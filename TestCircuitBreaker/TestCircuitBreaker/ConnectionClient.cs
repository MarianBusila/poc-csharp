using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCircuitBreaker
{
    public class ConnectionClient
    {
        public int GetListOfItems(int requestCode)
        {
            int responseCode = 1;
            if((requestCode > 3 && requestCode < 7) || (requestCode == 17)) //around the 17 request, the circuit will be in HalfOpen
                Console.WriteLine($"GetListOfItems with request code {requestCode} returned {responseCode}");
            else
                throw new ArgumentNullException();
            
            return responseCode;
        }
    }
}
