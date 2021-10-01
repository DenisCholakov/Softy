using System;
using System.Linq;
using System.Text;

namespace Messaging
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] arr = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            string text = Console.ReadLine();
            
            StringBuilder output = new StringBuilder();
            for (int i = 0; i < arr.Length; i++)
            {
                int sum = 0;
                while (arr[i] != 0)
                {
                    sum += arr[i] % 10;
                    arr[i] /= 10;
                }

                while (sum >= text.Length)
                {
                    sum -= text.Length;
                }
                output.Append(text[sum]);
                text = text.Remove(sum, 1);
            }

            Console.WriteLine(output);
        }
    }
}
