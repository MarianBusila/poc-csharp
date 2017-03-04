using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNewtonsoftJson
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Serialize object");
            Console.WriteLine(JSONHelper.JSONSerialize());

            Console.WriteLine("Deserialize object");
            Console.WriteLine(JSONHelper.JSONDeserialize());
        }
    }
}
