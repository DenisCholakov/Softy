using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

// Тук може и без out параметър като връщаме int и проверяваме дали е -1
namespace EqualArrays
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input1 = Console.ReadLine().Split();
            string[] input2 = Console.ReadLine().Split();

            if (input1.Length == input2.Length)
            {
                bool areEqual = EqCheck(input1.Length, input1, input2, out int diffPos);

                if (areEqual)
                {
                    int sum = input1.Select(int.Parse).ToArray().Sum();
                    Console.WriteLine($"Arrays are identical. Sum: {sum}");
                }
                else
                {
                    Console.WriteLine($"Arrays are not identical. Found difference at {diffPos} index.");
                }
            }
            else
            {
                int length = input1.Length < input2.Length ? input1.Length : input2.Length;
                bool areEqual = EqCheck(length, input1, input2, out int diffPos);

                if (areEqual)
                {
                    Console.WriteLine($"Arrays are not identical. Found difference at {length} index.");
                }
                else
                {
                    Console.WriteLine($"Arrays are not identical. Found difference at {diffPos} index.");
                }
            }
        }

        public static bool EqCheck(int n, string[] input1, string[] input2, out int diffPos)
        {
            for (int i = 0; i < n; i++)
            {
                if (input1[i] != input2[i])
                {
                    diffPos = i;
                    return false;
                }
            }

            diffPos = -1;
            return true;
        }
    }
}
