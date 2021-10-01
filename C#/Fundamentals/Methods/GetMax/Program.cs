using System;
using System.ComponentModel.DataAnnotations;

namespace GetMax
{
    class Program
    {
        static void Main(string[] args)
        {
            string valueType = Console.ReadLine();

            switch (valueType)
            {
                case "int":
                    int a = int.Parse(Console.ReadLine());
                    int b = int.Parse(Console.ReadLine());
                    Console.WriteLine(GetMax(a, b));
                    break;
                case "char":
                    char c = char.Parse(Console.ReadLine());
                    char d = char.Parse(Console.ReadLine());
                    Console.WriteLine(GetMax(c, d));
                    break;
                case "string":
                    string str1 = Console.ReadLine();
                    string str2 = Console.ReadLine();
                    Console.WriteLine(GetMax(str1, str2));
                    break;
            }
        }

        private static int GetMax(int a, int b)
        {
            return a > b ? a : b;
        }

        private static char GetMax(char a, char b)
        {
            return a > b ? a : b;
        }

        private static string GetMax(string a, string b)
        {
            int length = a.Length > b.Length ? b.Length : a.Length;
            for (int i = 0; i < length; i++)
            {
                if (a[i] > b[i])
                {
                    return a;
                }
                else
                {
                    return b;
                }
            }

            return a.Length == 0 ? b : a;
        }
    }
}
