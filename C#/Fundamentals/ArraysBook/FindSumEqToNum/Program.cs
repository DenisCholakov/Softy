using System;
using System.Linq;

namespace FindSumEqToNum
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] nums = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int S = int.Parse(Console.ReadLine());

            int index = 0;
            int numCount = 0;
            bool isFound = false;
            for (int k = 0; k < nums.Length; k++)
            {
                if (!isFound)
                {
                    for (int i = 0; i < nums.Length - k; i++)
                    {
                        int sum = 0;
                        for (int j = 0; j < k + 1; j++)
                        {
                            sum += nums[i + j];
                        }

                        if (sum == S)
                        {
                            index = i;
                            numCount = k + 1;
                            isFound = true;
                        }
                    }
                }
            }

            for (int i = index; i < index + numCount; i++)
            {
                Console.Write(nums[i] + " ");
            }
        }
    }
}
