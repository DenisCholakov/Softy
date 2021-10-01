using System;
using System.Linq;

namespace MostFrequentElement
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] nums = Console.ReadLine().Split().Select(int.Parse).ToArray();

            int minIndex = 0;
            int temp = 0;
            for (int i = 0; i < nums.Length - 1; i++)
            {
                minIndex = i;
                for (int j = i+1; j < nums.Length; j++)
                {
                    if (nums[j] < nums[minIndex])
                    {
                        minIndex = j;
                    }
                }

                if (minIndex != i)
                {
                    temp = nums[i];
                    nums[i] = nums[minIndex];
                    nums[minIndex] = temp;
                }
            }

            int mostFreqNum = 0;
            int mostRepeats = 0;
            int repeats = 1;
            for (int i = 0; i < nums.Length - 1; i++)
            {
                if (nums[i] == nums[i+1])
                {
                    repeats++;
                }
                else
                {
                    repeats = 1;
                }

                if (repeats > mostRepeats)
                {
                    mostRepeats = repeats;
                    mostFreqNum = nums[i];
                }
            }

            Console.WriteLine(mostFreqNum);
        }
    }
}
