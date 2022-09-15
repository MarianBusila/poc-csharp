using System;

namespace Codility
{
    public class Tree
    {

        public int x;
        public Tree l;
        public Tree r;

    }
    public class TreeHeight
    {
        public static int Solution(Tree T)
        {
            if (T == null)
                return 0;

            int leftHeight = Solution(T.l);
            int rightHeight = Solution(T.r);
            return Math.Max(leftHeight, rightHeight) + 1;
        }
    }
}
