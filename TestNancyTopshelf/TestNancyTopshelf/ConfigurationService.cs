using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNancyTopshelf
{
    class ConfigurationService : IConfigurationService
    {
        public string GetValue(string key)
        {
            string value = "Undefined";
            switch(key)
            {
                case "HostURL":
                    value = "http://localhost:5000";
                    break;
            }
            return value;
        }
    }
}
