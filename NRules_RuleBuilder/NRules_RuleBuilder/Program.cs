using NRules;
using System;

namespace NRules_RuleBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            var repository = new CustomRuleRepository();
            repository.LoadRules();
            ISessionFactory factory = repository.Compile();

            ISession session = factory.CreateSession();
            var customer = new Customer("John Do");
            session.Insert(customer);
            session.Insert(new Order(customer, 90.00m));
            session.Insert(new Order(customer, 110.00m));

            session.Fire();
        }
    }
}
