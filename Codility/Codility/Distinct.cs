using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codility
{
    public class Distinct
    {
        public static int Solution(int[] A)
        {
            // HashSet<int> hs = new HashSet<int>(A);
            // return hs.Count;
            if (A.Length == 0)
                return 0;

            Array.Sort(A);
            int count = 1;
            for(int i =0 ; i < A.Length - 1 ; i++)
            {
                if (A[i] != A[i + 1])
                    count++;
            }
            return count;
        }        
    }
}
