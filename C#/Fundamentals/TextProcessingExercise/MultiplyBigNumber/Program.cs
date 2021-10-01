using System;
using System.Text;
using System.Collections;

namespace MultiplyBigNumber
{
    class Program
    {
        static void Main(string[] args)
        {
            string bigNumber = Console.ReadLine().TrimStart('0');
            int multiplyer = int.Parse(Console.ReadLine());
            Stack result = new Stack();

            if (multiplyer == 0)
            {
                System.Console.WriteLine(0);
                return;
            }

            int reminder = 0;
            for (int i = bigNumber.Length - 1; i >= 0; i--)
            {
                result.Push((int.Parse(bigNumber[i].ToString()) * multiplyer + reminder) % 10);
                reminder = (int.Parse(bigNumber[i].ToString()) * multiplyer + reminder) / 10;
            }

            if (reminder != 0)
            {
                result.Push(reminder);
            }

            while (result.Count > 0)
            {
                System.Console.Write(result.Pop());
            }
        }
    }
}
