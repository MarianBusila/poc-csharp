using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codility
{
    public class CyclicRotation
    {
        public static int[] Solution(int[] A, int K)
        {
            if (A.Length == 0)
                return A;
            for(int i = 0; i < K; i++)
            {
                int temp = A[A.Length - 1];
                for(int j = A.Length - 1; j > 0; j--)
                {
                    A[j] = A[j - 1];
                }
                A[0] = temp;
            }
            return A;
        }

    }
}
