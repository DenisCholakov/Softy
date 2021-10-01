using System;
using System.Linq;

namespace EncryptSortPrint
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            int[] nameVal = new int[n];
            char[] vowlChars = new char[] {'a', 'e', 'i', 'o', 'u', 'A', 'E', 'I', 'O', 'U'};

            string name = "";
            for (int i = 0; i < n; i++)
            {
                name = Console.ReadLine();
                for (int j = 0; j < name.Length; j++)
                {
                    if (vowlChars.Contains(name[j]))
                    {
                        nameVal[i] += name[j] * name.Length;
                    }
                    else
                    {
                        nameVal[i] += name[j] / name.Length;
                    }
                }
            }

            Array.Sort(nameVal);

            for (int i = 0; i < nameVal.Length; i++)
            {
                Console.WriteLine(nameVal[i]);
            }
        }
    }
}
