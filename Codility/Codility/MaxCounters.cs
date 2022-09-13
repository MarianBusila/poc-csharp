using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codility
{
    public class MaxCounters
    {
        public static int[] Solution(int N, int[] A)
        {
            int[] result = new int[N];
            int maxCounter = 0;
            for(int i = 0 ; i < A.Length ; i++)
            {
                if(A[i] >= 1 && A[i] <= N)
                {
                    result[A[i] - 1] += 1;
                    if (result[A[i] - 1] > maxCounter)
                        maxCounter = result[A[i] - 1];
                }
                else
                if(A[i] == N + 1)
                {
                    for (int j = 0 ; j < N ; j++)
                        result[j] = maxCounter;
                }
                Console.WriteLine(String.Join(", ", result));
            }
            return result;
        }        
    }
}
