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

        #region AggregateOperators
        static void AggregateSumSquareRoots()
        {
            IEnumerable<int> numbers = new List<int> { 1, 2, 3 };
            var result = numbers.Sum(x => x * x);
            Console.WriteLine("AggregateSumSquareRoots:" + result);
        }

        static void AggregateCountStringsWithLenghtMoreThanFive()
        {
            string[] words = { "cherry", "apple", "blueberry" };
            var result = words.Count(w => w.Length > 5);
            Console.WriteLine("AggregateCountStringsWithLenghtMoreThanFive:" + result);
        }

        static void AggregateMultiplyAllElements()
        {
            IEnumerable<int> numbers = new List<int> { 1, 2, 3, 4 };
            var result = numbers.Aggregate((currentProduct, nextFactor) => currentProduct * nextFactor);
            Console.WriteLine("AggregateMultiplyAllElements:" + result);
        }
        #endregion

        #region ProjectionOperators
        static void ProjectionToUpper()
        {
            string[] words = { "cherry", "apple", "blueberry" };
            var result = string.Join(", ", words.Select(x => x.ToUpper()).ToArray());
            Console.WriteLine("ProjectionToUpper:" + result);
        }

        static void ProjectionListAllProductsNamesFromCategoryBeverages()
        {
            IEnumerable<Product> products = Product.GetProducts();
            var result = string.Join(", ", products.Where(x => x.Category == "Beverages").Select(x => x.ProductName).ToArray());
            Console.WriteLine("ProjectionListAllProductsNamesFromCategoryBeverages:" + result);
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

            Console.WriteLine("Aggregate Operators!");
            Console.WriteLine("______________________");
            AggregateSumSquareRoots();
            AggregateCountStringsWithLenghtMoreThanFive();
            AggregateMultiplyAllElements();
            Console.WriteLine("______________________");
            Console.WriteLine();

            Console.WriteLine("Projections Operators!");
            Console.WriteLine("______________________");
            ProjectionToUpper();
            ProjectionListAllProductsNamesFromCategoryBeverages();
            Console.WriteLine("______________________");
            Console.WriteLine();
        }
    }
}
