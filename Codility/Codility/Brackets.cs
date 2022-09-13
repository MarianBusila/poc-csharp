using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codility
{
    public class Brackets
    {
        public static int Solution(string S)
        {
            if (S == String.Empty)
                return 1;

            Stack<char> stack = new Stack<char>();

            foreach(var c in S)
            {
                if(stack.Count == 0)
                    stack.Push(c);
                else
                {
                    char top = stack.Peek();
                    if ((top == '(' && c == ')') || (top == '[' && c == ']') || (top == '{' && c == '}'))
                        stack.Pop();
                    else
                        stack.Push(c);
                }
            }

            return stack.Count == 0 ? 1 : 0;
        }        
    }
}
