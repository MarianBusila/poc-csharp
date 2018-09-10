using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codility
{
    class OddOccurrencesInArray
    {
        public static int Solution(int[] A)
        {
            int unMatchNumber = A[0];
            for (int i = 1; i < A.Length; i++)
            {
                unMatchNumber = unMatchNumber ^ A[i];
            }
            return unMatchNumber;
        }
    }
}
