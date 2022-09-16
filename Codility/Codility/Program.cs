using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codility
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            int value = BinaryGap.Solution(20);
            Console.WriteLine("BinaryGap: " + value);
            */

            /*
            int[] array = new int[9] { 2, 3, 5, 3, 5, 2, 7, 4, 4 };
            int value = OddOccurrencesInArray.Solution(array);
            Console.WriteLine("OddOccurrencesInArray: " + value);
            */

            /*
            int[] array = new int[] { 1, 2, 3, 4, 5 };
            int[] rotated = CyclicRotation.Solution(array, 6);
            Console.WriteLine("CyclicRotation: " + string.Join(",", rotated));
            */

            /*
            int[] array = new int[] { 1, 2, 3, 5 };
            int value = PermMissingElem.Solution(array);
            Console.WriteLine("PermMissingElem: " + value);
            */

            /*
            int n = 1234;
            int value = Task1.Solution(n);
            Console.WriteLine("Task1: " + value);
            */

            /*
            string s = "";
            string result = Task2.Solution(s);
            Console.WriteLine("Task2: " + result);
            */

            /*
            int result = Task3.Solution(955);
            Console.WriteLine("Task3: " + result);
            */

            /*
            int result = ConsecSum.Solution2(9);
            Console.WriteLine("ConsecSum 9: " + result);
            result = ConsecSum.Solution2(15);
            Console.WriteLine("ConsecSum 15: " + result);
            result = ConsecSum.Solution2(36);
            Console.WriteLine("ConsecSum 36: " + result);
            */
            /*
            int[,] result = DiagonalMatrixSum.Solution(new int[,] { {1, 2, 3}, {1, 2, 1}, {2, 3, 4} });                        
            Console.WriteLine("DiagonalMatrixSum:" + DiagonalMatrixSum.MatrixToString(result));
            */

            /*
            int value = FrogJump.Solution(10, 85, 30);
            Console.WriteLine("FrogJump: " + value);
            */

            /*
            int value = TapeEquilibrum.Solution(new int[] {3, 1, 2, 4, 3});
            Console.WriteLine("TapeEquilibrum: " + value);
            */

            /*
            int value = FrogRiverOne.Solution(5, new []{1, 3, 1, 4, 2, 3, 5, 4});
            Console.WriteLine("FrogRiverOne: " + value);
            */

            /*
            int value = PermCheck.Solution(new[] { 4, 1, 3, 2 });
            Console.WriteLine("PermCheck: " + value);
            */

            /*
            int[] result = MaxCounters.Solution(5, new[] { 3, 4, 4, 6, 1, 4, 4, 6, 7 });
            Console.WriteLine("PermCheck: " + String.Join(", ", result));
            */

            /*
            int value = Distinct.Solution(new[] { 2, 1, 1, 2, 3, 1 });
            Console.WriteLine("Distinct: " + value);
            */

            /*
            int value = Brackets.Solution("{[()()]}");
            Console.WriteLine("Brackets: " + value);
            */

            /*
            var tree = new Tree
            {
                x = 5,
                l = new Tree { x = 3, l = new Tree { x = 20}, r = new Tree { x = 21, r = new Tree {x=5} }},
                r = new Tree { x = 10, l = new Tree {x = 1}}

            };
            int value = TreeHeight.Solution(tree);
            Console.WriteLine("TreeHeight: " + value);
            */

            /*
            int value = ArrayInversionCount.Solution(new[] { -1, 6, 3, 4, 7, 4 });
            //int value = ArrayInversionCount.Solution(new int[] {});
            Console.WriteLine("ArrayInversionCount: " + value);
            */

            
            var vertices = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var edges = new[] 
            { 
                Tuple.Create(1, 2), Tuple.Create(1, 3),
                Tuple.Create(2, 4), Tuple.Create(3, 5), Tuple.Create(3, 6),
                Tuple.Create(4, 7), Tuple.Create(5, 7), Tuple.Create(5, 8), Tuple.Create(5, 6),
                Tuple.Create(8, 9), Tuple.Create(8, 10), Tuple.Create(9, 10)
            };
            var graph = new Graph<int>(vertices, edges);
            var algorithms = new GraphAlgorithms();

            // BFS
            /*
            Console.WriteLine("BFS: " + string.Join(",", algorithms.BFS<int>(graph, 1)));

            var shortestPath = algorithms.ShortestPathFunction(graph, 1);
            foreach (int vertex in vertices)
            {
                Console.WriteLine($"shortestPath for {vertex}: {string.Join(",", shortestPath(vertex))}");
            }
            */

            // DFS
            Console.WriteLine("DFS: " + string.Join(",", algorithms.DFS<int>(graph, 1)));
        }


    }
}
