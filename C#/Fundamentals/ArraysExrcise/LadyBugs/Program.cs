using System;
using System.Linq;

namespace LadyBugs
{
    class Program
    {
        static void Main(string[] args)
        {
            short n = short.Parse(Console.ReadLine());
            byte[] field = new byte[n];

            int[] bugIndexes = Console.ReadLine().Split().Select(int.Parse).ToArray();
            foreach (var index in bugIndexes)
            {
                if (index < field.Length && index >= 0)
                {
                    field[index] = 1;
                }
            }

            string input = Console.ReadLine();
            while (input != "end")
            {
                string[] command = input.Split();
                int pos = int.Parse(command[0]);
                if (pos >= field.Length || pos < 0)
                {
                    input = Console.ReadLine();
                    continue;
                }
                int flyLength = int.Parse(command[2]);
                if (field[pos] == 1)
                {
                    field[pos] = 0;
                    bool canMove = false;
                    while (!canMove)
                    {
                        if (command[1] == "right")
                        {
                            if (pos+flyLength < field.Length)
                            {
                                pos += flyLength;

                                if (field[pos] == 0)
                                {
                                    field[pos] = 1;
                                    canMove = true;
                                }
                            }
                            else
                            {
                                canMove = true;
                            }
                        }
                        else
                        {
                            if (pos - flyLength >= 0)
                            {
                                pos -= flyLength;

                                if (field[pos] == 0)
                                {
                                    field[pos] = 1;
                                    canMove = true;
                                }
                            }
                            else
                            {
                                canMove = true;
                            }
                        }
                    }
                }
                input = Console.ReadLine();
            }

            Console.WriteLine(String.Join(' ', field));
        }
    }
}
