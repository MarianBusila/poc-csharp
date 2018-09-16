using Domain;
using NRules.Fluent.Dsl;
using System.Collections.Generic;
using System.Linq;

namespace TestNRules
{
    public class PreferredCustomerDiscountRule : Rule
    {
        public override void Define()
        {
            Customer customer = null;
            IEnumerable<Order> orders = null;

            When()
                .Match<Customer>(() => customer, c => c.IsPreferred)
                .Query(() => orders, x => x
                    .Match<Order>(o => o.Customer == customer,
                                  o => !o.IsDiscounted)
                    .Collect()
                    .Where(c => c.Any()));

            Then()
                .Do(ctx => ApplyDiscount(orders, 10.0))
                .Do(ctx => ctx.UpdateAll(orders));
        }

        private static void ApplyDiscount(IEnumerable<Order> orders, double discount)
        {
            foreach (var order in orders)
            {
                order.ApplyDiscount(discount);
            }
        }
    }
}
