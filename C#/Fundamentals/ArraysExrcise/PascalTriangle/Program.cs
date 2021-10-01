using System;

namespace PascalTriangle
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());

            int[][] pTriangle = new int[n][];
            pTriangle[0] = new int[1] {1};
            for (int i = 1; i < n; i++)
            {
                pTriangle[i] = new int[i+1];
                pTriangle[i][0] = 1;
                pTriangle[i][i] = 1;

                for (int j = 1; j < i; j++)
                {
                    pTriangle[i][j] = pTriangle[i - 1][j - 1] + pTriangle[i-1][j];
                }
            }

            foreach (var arr in pTriangle)
            {
                Console.WriteLine(String.Join(' ', arr));
            }
        }
    }
}
