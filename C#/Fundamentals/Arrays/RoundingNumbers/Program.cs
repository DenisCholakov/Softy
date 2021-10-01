using System;

namespace RoundingNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            string[] strNums = input.Split(' ');

            float num = 0;
            for (int i = 0; i < strNums.Length; i++)
            {
                num = float.Parse(strNums[i]);
                Console.WriteLine($"{Convert.ToDecimal(num)} => {Convert.ToDecimal(Math.Round(num,MidpointRounding.AwayFromZero))}");
            }
        }
    }
}
