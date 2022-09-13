using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codility
{
    public class FrogRiverOne
    {
        public static int Solution(int X, int[] A)
        {
            bool[] has = new bool[X];
            int count = 0;
            for (int i = -1; ++i < A.Length;)
            {
                if(!has[A[i] - 1])
                {
                    has[A[i] - 1] = true;
                    if (++count == X)
                        return i;
                }
            }
            return -1;
        }        
    }
}
