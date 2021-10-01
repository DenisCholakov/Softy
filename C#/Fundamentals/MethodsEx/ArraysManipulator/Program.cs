using System;
using System.Linq;

namespace ArraysManipulator
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] arr = Console.ReadLine().Split().Select(int.Parse).ToArray();

            string input = string.Empty;
            while ((input=Console.ReadLine()) != "end")
            {
                string[] command = input.Split();

                if (command[0] == "exchange")
                {
                    int index = int.Parse(command[1]);
                    Exchange(arr, index);
                }
                else if (command[0] == "max")
                {
                    bool isEven = command[1] == "even";
                    int index = 0;
                    if (isEven)
                    {
                        index = IndexMaxEven(arr);
                    }
                    else
                    {
                        index = IndexMaxOdd(arr);
                    }

                    if (index < 0)
                    {
                        Console.WriteLine("No matches");
                    }
                    else
                    {
                        Console.WriteLine(index);
                    }
                }
                else if (command[0] == "min")
                {
                    bool isEven = command[1] == "even";
                    int index = 0;
                    if (isEven)
                    {
                        index = IndexMinEven(arr);
                    }
                    else
                    {
                        index = IndexMinOdd(arr);
                    }

                    if (index < 0)
                    {
                        Console.WriteLine("No matches");
                    }
                    else
                    {
                        Console.WriteLine(index);
                    }
                }
                else if (command[0] == "first")
                {
                    int count = int.Parse(command[1]);
                    if (command[2] == "even")
                    {
                        FindFirstEven(arr, count);
                    }
                    else
                    {
                        FindFirstOdd(arr, count);
                    }
                }
                else if (command[0] == "last")
                {
                    int count = int.Parse(command[1]);
                    if (command[2] == "even")
                    {
                        FindLastEven(arr, count);
                    }
                    else
                    {
                        FindLastOdd(arr, count);
                    }
                }
            }

            Console.WriteLine("[" + String.Join(", ", arr) + "]");
        }

        private static void FindLastEven(int[] arr, int count)
        {
            if (count > arr.Length)
            {
                Console.WriteLine("Invalid count");
                return;
            }

            string elements = string.Empty;

            int counter = 0;
            for (int i = arr.Length - 1; i >= 0; i--)
            {
                if (arr[i] % 2 == 0)
                {
                    elements += arr[i] + " ";
                    counter++;
                    if (counter == count)
                    {
                        break;
                    }
                }
            }

            var result = elements.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (counter == 0)
            {
                Console.WriteLine("[]");
            }
            else
            {
                Console.WriteLine("[" + String.Join(", ", result) + "]");
            }
        }

        private static void FindLastOdd(int[] arr, int count)
        {
            if (count > arr.Length)
            {
                Console.WriteLine("Invalid count");
                return;
            }

            string elements = string.Empty;

            int counter = 0;
            for (int i = arr.Length - 1; i >= 0; i--)
            {
                if (arr[i] % 2 != 0)
                {
                    elements += arr[i] + " ";
                    counter++;
                    if (counter == count)
                    {
                        break;
                    }
                }
            }

            var result = elements.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (counter == 0)
            {
                Console.WriteLine("[]");
            }
            else
            {
                Console.WriteLine("[" + String.Join(", ", result) + "]");
            }
        }

        private static void FindFirstOdd(int[] arr, int count)
        {
            if (count > arr.Length)
            {
                Console.WriteLine("Invalid count");
                return;
            }

            string elements = string.Empty;

            int counter = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] % 2 != 0)
                {
                    elements += arr[i] + " ";
                    counter++;
                    if (counter == count)
                    {
                        break;
                    }
                }
            }

            var result = elements.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (counter == 0)
            {
                Console.WriteLine("[]");
            }
            else
            {
                Console.WriteLine("[" + String.Join(", ", result) + "]");
            }
        }

        private static void FindFirstEven(int[] arr, int count)
        {
            if (count > arr.Length)
            {
                Console.WriteLine("Invalid count");
                return;
            }

            string elements = string.Empty;

            int counter = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] % 2 == 0)
                {
                    elements += arr[i] + " ";
                    counter++;
                    if (counter == count)
                    {
                        break;
                    }
                }
            }

            var result = elements.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (counter == 0)
            {
                Console.WriteLine("[]");
            }
            else
            {
                Console.WriteLine("[" + String.Join(", ", result) + "]");
            }
        }

        private static int IndexMinOdd(int[] arr)
        {
            int minOdd = int.MaxValue;
            int index = -1;

            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] % 2 != 0)
                {
                    if (arr[i] <= minOdd)
                    {
                        minOdd = arr[i];
                        index = i;
                    }
                }
            }

            return index;
        }

        private static int IndexMinEven(int[] arr)
        {
            int minEven = int.MaxValue;
            int index = -1;

            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] % 2 == 0)
                {
                    if (arr[i] <= minEven)
                    {
                        minEven = arr[i];
                        index = i;
                    }
                }
            }

            return index;
        }

        private static int IndexMaxOdd(int[] arr)
        {
            int maxOdd = int.MinValue;
            int index = -1;

            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] % 2 != 0)
                {
                    if (arr[i] >= maxOdd)
                    {
                        maxOdd = arr[i];
                        index = i;
                    }
                }
            }

            return index;
        }

        private static int IndexMaxEven(int[] arr)
        {
            int maxEven = int.MinValue;
            int index = -1;

            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] % 2 == 0)
                {
                    if (arr[i] >= maxEven)
                    {
                        maxEven = arr[i];
                        index = i;
                    }
                }
            }

            return index;
        }

        private static void Exchange(int[] arr, int index)
        {
            if (index >= arr.Length || index < 0)
            {
                Console.WriteLine("Invalid index");
                return;
            }

            int[] tempArr1 = new int[index + 1];
            int[] tempArr2 = new int[arr.Length - index - 1];

            for (int i = 0; i < tempArr1.Length; i++)
            {
                tempArr1[i] = arr[i];
            }

            for (int i = 0; i < tempArr2.Length; i++)
            {
                tempArr2[i] = arr[i + index + 1];
            }

            for (int i = 0; i < tempArr2.Length; i++)
            {
                arr[i] = tempArr2[i];
            }

            for (int i = 0; i < tempArr1.Length; i++)
            {
                arr[i + tempArr2.Length] = tempArr1[i];
            }

        }
    }
}
