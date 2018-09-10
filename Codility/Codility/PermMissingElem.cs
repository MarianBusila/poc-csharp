using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codility
{
    public class PermMissingElem
    {
        public static int Solution(int[] A)
        {
            double n = A.Length + 1;            
            double sumN =  (double)(n * (n + 1) / 2);
            for (int i = 0; i < A.Length; i++)
            {
                sumN -= A[i];
            }                        
            return  (int)sumN;
        }
    }
}
