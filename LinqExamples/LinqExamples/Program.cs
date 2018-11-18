using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqExamples
{
    class Program
    {
        #region RestrictionOperators
        static void WhereLessThanFive()
        {
            IEnumerable<int> numbers = new List<int>{5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            var result = string.Join(",", numbers.Where(x => x < 5).ToArray());
            Console.WriteLine("WhereLessThanFive:" + result);
        }

        static void WhereInStockAndCostMoreThanThree()
        {
            IEnumerable<Product> products = Product.GetProducts();
            var result = string.Join<Product>(Environment.NewLine, products.Where(x => x.UnitsInStock > 0 && x.UnitPrice > 3.00M).ToArray());
            Console.WriteLine("WhereInStockAndCostMoreThanThree:" + result);
        }
        #endregion

        static void Main(string[] args)
        {
            Console.WriteLine("Restriction Operators!");
            Console.WriteLine("______________________");
            WhereLessThanFive();
            WhereInStockAndCostMoreThanThree();
            Console.WriteLine("______________________");
            Console.WriteLine();
        }
    }
}
