using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codility
{
    public class DiagonalMatrixSum
    {
        public static int[,] Solution(int[,] matrix)
        {
            int n = matrix.GetLength(0);
            int m = matrix.GetLength(1);
            int[,] result = new int[n, m];
            
            for (int i = 0; i < n; i++)
            {
                int rowSum = 0;
                for (int j = 0; j < m; j++)
                {
                    rowSum += matrix[i, j];
                    result[i, j] = rowSum + result[i == 0? 0: i - 1, j];
                }
            }
            return result;
        }

        public static string MatrixToString(int[,] result)
        {
            var sb = new StringBuilder(string.Empty);
            var maxI = result.GetLength(0);
            var maxJ = result.GetLength(1);
            for (var i = 0; i < maxI; i++)
            {
                sb.Append(",{");
                for (var j = 0; j < maxJ; j++)
                {
                    sb.Append($"{result[i, j]},");
                }

                sb.Append("}");
            }

            sb.Replace(",}", "}").Remove(0, 1);
            return sb.ToString();
        }
    }
}
