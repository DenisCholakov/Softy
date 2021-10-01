using System;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace charCompare
{
    class Program
    {
        static void Main(string[] args)
        {
            char[] charArr1 = Console.ReadLine().Split().Select(char.Parse).ToArray();
            char[] charArr2 = Console.ReadLine().Split().Select(char.Parse).ToArray();

            if (charArr1.Length == charArr2.Length)
            {
                for (int i = 0; i < charArr1.Length; i++)
                {
                    if (charArr1[i] != charArr2[i])
                    {
                        char[] firstArr = (int) charArr1[i] > (int) charArr2[i] ? charArr2 : charArr1;
                        foreach (var ch in firstArr)
                        {
                            Console.Write(ch + ' ');
                        }
                        return;
                    }
                }
                Console.WriteLine(String.Join(' ', charArr1));
            }
            else
            {
                char[] shorterArray = charArr1.Length < charArr2.Length ? charArr1 : charArr2;
                for (int i = 0; i < shorterArray.Length; i++)
                {
                    if (charArr1[i] != charArr2[i])
                    {
                        char[] firstArr = (int)charArr1[i] > (int)charArr2[i] ? charArr2 : charArr1;
                        Console.WriteLine(String.Join(' ',firstArr));
                        return;
                    }
                }

                char[] longerArray = charArr1.Length > charArr2.Length ? charArr1 : charArr2;
                Console.WriteLine(String.Join(' ',shorterArray));
            }
        }
    }
}
