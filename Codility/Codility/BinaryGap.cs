using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codility
{
    public class BinaryGap
    {
        public static int Solution(int n)
        {
            // shift to remove the trailing zeros  
            if (n != 0)
            {
                while ((n & 1) == 0)
                {
                    n >>= 1;
                }
            }

            int count = 0;
            int max = 0;
            for (int i = 1; i < 32; i++)
            {
                if ((n & (1 << i)) == 0)
                {
                    count++;
                }
                else
                {
                    if (count > max)
                        max = count;
                    count = 0;
                }
            }

            return max;
        }        
    }
}
