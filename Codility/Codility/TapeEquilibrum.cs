using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codility
{
    public class TapeEquilibrum
    {
        public static int Solution(int[] A)
        {
            int sumLeft = A[0];
            int sumRight = 0;

            for (int i = 1 ; i < A.Length ; i++)
                sumRight += A[i];
            int min = Math.Abs(sumLeft - sumRight);

            for(int i = 1 ; i < A.Length - 1 ; i++)
            {
                sumLeft += A[i];
                sumRight -= A[i];

                var absValue = Math.Abs(sumLeft - sumRight);
                if (min > absValue)
                    min = absValue;
            }
            return min;
        }        
    }
}
