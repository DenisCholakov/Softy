using System;

namespace PascalTriangle
{
    class Program
    {
        static void Main(string[] args)
        {
            int size = int.Parse(Console.ReadLine());
            long[][] triangle = new long[size][];
            
            for (int i = 0; i < size; i++)
            {
                triangle[i] = new long[i + 1];
                triangle[i][0] = 1;
                triangle[i][i] = 1;

                for (int j = 1; j < i; j++)
                {
                    triangle[i][j] = triangle[i - 1][j] + triangle[i - 1][j - 1]; 
                }
            }

            foreach (var line in triangle)
            {
                Console.WriteLine(String.Join(' ', line));
            }
        }
    }
}
