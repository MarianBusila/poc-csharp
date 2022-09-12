using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codility
{
    public class FrogJump
    {
        public static int Solution(int X, int Y, int D)
        {
            return (int)Math.Ceiling((Y - (double)X) / D);
        }        
    }
}
