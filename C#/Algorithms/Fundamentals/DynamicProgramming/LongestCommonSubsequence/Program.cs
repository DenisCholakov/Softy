using System;
using System.IO;

namespace LongestCommonSubsequence
{
    class Program
    {
        static void Main(string[] args)
        {
            var str1 = Console.ReadLine();
            var str2 = Console.ReadLine();

            var lcs = new int[str1.Length + 1, str2.Length + 1];

            for (int i = 1; i < lcs.GetLength(0); i++)
            {
                for (int j = 1; j < lcs.GetLength(1); j++)
                {
                    if (str1[i - 1] == str2[j - 1])
                    {
                        lcs[i, j] = lcs[i - 1, j - 1] + 1;
                    }
                    else
                    {
                        lcs[i, j] = Math.Max(lcs[i - 1, j], lcs[i, j - 1]);
                    }
                }
            }

            Console.WriteLine(lcs[str1.Length, str2.Length]);
        }
    }
}
