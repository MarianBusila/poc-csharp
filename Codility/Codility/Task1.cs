using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codility
{
    public class Task1
    {
        public static int Solution(int N)
        {
            var charArray = N.ToString().ToCharArray();
            Array.Sort(charArray);
            Array.Reverse(charArray);
            return Convert.ToInt32(new string(charArray));
        }
    }
}
