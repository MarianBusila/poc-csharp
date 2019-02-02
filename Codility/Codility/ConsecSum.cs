using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codility
{
    public class ConsecSum
    {
        public static int Solution(int n)
        {
            int solutionCount = 1;
            int start = 1; int end = start + 1;
            int currentSum = start + end;
            while (start <= n / 2)
            {
                if (currentSum >= n) // we went over
                {
                    if (currentSum == n)
                    {
                        solutionCount++;
                        string str = string.Empty;
                        for (int i = start; i <= end; i++)
                            str = str + i + "+";
                        Console.WriteLine(str);

                    }
                    currentSum -= start;
                    currentSum -= end;
                    start++;
                    end--;
                }
                else // we can increment the end
                {
                    end++;
                    currentSum += end;
                }
            }

            return solutionCount;
        }

        public static int Solution2(int n)
        {
            var solutionCount = 0;
            for ( int k = 1; k <= n; k++)
            {
                if(2 * n % k == 0)
                {
                    if((2 * n / k - k - 1) % 2 == 0 && (2 * n / k - k - 1) >= 0)
                    {
                        var x = (2 * n / k - k - 1) / 2;
                        solutionCount++;
                        string str = string.Empty;
                        for (int i = 1; i <= k; i++)
                            str = str + (x + i) + "+";
                        Console.WriteLine(str);
                    }
                }
            }
            return solutionCount;
        }
    }
}
