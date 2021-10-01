using System;

namespace CharacterMultiplier
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = Console.ReadLine().Split();

            string longer = input[0].Length > input[1].Length ? input[0] : input[1];
            string shorter = input[0].Length <= input[1].Length ? input[0] : input[1];

            int sum = 0;
            for (int i = 0; i < shorter.Length; i++)
            {
                sum += longer[i] * shorter[i];
            }

            for (int i = shorter.Length; i < longer.Length; i++)
            {
                sum += longer[i];
            }

            System.Console.WriteLine(sum);
        }
    }
}
