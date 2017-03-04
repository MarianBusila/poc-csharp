using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TestNewtonsoftJson
{
    class JSONHelper
    {
        public static string JSONSerialize()
        {
            Employee employee = new Employee() { ID = 1, Name = "Marian", Address = "Montreal" };
            return JsonConvert.SerializeObject(employee);
        }

        public static string JSONDeserialize()
        {
            string json = @"{'ID' : '1', 'Name' : 'Marian', 'Address' : 'Montreal'}";
            Employee employee = JsonConvert.DeserializeObject<Employee>(json);
            return employee.ToString();
        }
    }
}
