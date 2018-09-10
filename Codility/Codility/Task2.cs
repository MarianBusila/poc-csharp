using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codility
{
    public class Task2
    {
        public static string Solution(string S)
        {
            char[] buffer = new char[S.Length];            
            int indexBuffer = 0;
            for ( int indexS = 0; indexS < S.Length; indexS++)
            {
                if(indexBuffer > 0 && S[indexS] == buffer[indexBuffer - 1])
                {
                    // remove last element from buffer since we have a duplicate
                    indexBuffer--;
                }
                else
                {
                    // different, add it to the buffer
                    buffer[indexBuffer++] = S[indexS];
                }
            }
            return new string(buffer, 0, indexBuffer);
        }
    }
}
