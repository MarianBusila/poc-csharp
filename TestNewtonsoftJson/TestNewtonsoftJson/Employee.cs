using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNewtonsoftJson
{
    class Employee
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public override string ToString()
        {
            return string.Join(",", ID, Name, Address);
        }
    }
}
