using System;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace RepeatString
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = Console.ReadLine();
            int repeat = int.Parse(Console.ReadLine());

            Console.WriteLine(RepeatString(text, repeat));
        }

        private static StringBuilder RepeatString(string text, int rep)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < rep; i++)
            {
                sb.Append(text);
            }

            return sb;
        }
    }
}
