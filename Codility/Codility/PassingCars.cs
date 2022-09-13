using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codility
{
    public class PassingCars
    {
        public static int Solution(int[] A)
        {
            int eastCarCount = 0;
            int total = 0;
            for(int i = 0 ; i < A.Length ; i++)
            {
                if (A[i] == 0)
                    eastCarCount++;
                else
                {
                    total += eastCarCount;
                }

                if (total > 1000000000)
                    return -1;
            }
            return total;
        }        
    }
}
