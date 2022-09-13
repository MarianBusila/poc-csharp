using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codility
{
    public class PermCheck
    {
        public static int Solution(int[] A)
        {
			uint expected = (uint)(A.Length * (A.Length + 1) / 2), 
                sum = 0;
            HashSet<int> digits = new HashSet<int>(A);
            if (digits.Count != A.Length)
                return 0;
            for (int i = -1; ++i < A.Length; sum += (uint)A[i]) ;
            return sum == expected ? 1 : 0;
		}        
    }
}
