using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codility
{
    public class ArrayInversionCount
    {
        public static int Solution(int[] A)
        {
            int numberOfConversions = 0;

            // solution is O(n**2). Using merge sort, we can have an O(n *Log(n))
            for(int p = 0 ; p < A.Length - 1 ; p++)
                for(int q = p + 1 ; q < A.Length ; q++)
                    if (A[q] < A[p])
                        numberOfConversions++;

            if (numberOfConversions > 1000000000)
                return -1;

            return numberOfConversions;
        }        
    }
}
