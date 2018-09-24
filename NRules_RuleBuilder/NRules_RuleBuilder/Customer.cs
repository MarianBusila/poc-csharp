using System;
using System.Collections.Generic;
using System.Text;

namespace NRules_RuleBuilder
{
    public class Customer
    {
        public Customer(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}
