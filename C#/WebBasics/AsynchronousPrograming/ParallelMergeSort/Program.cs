using System;
using System.Linq;

namespace ParallelMergeSort
{
    class Program
    {
        static void Main(string[] args)
        {
            
        }

        private static int[] MergeSort(int[] arr)
        {
            if (arr.Length == 1)
            {
                return arr;
            }

            var middleIndex = arr.Length / 2;
            var leftHalf = arr.Take(middleIndex).ToArray();
            var rightHalf = arr.Skip(middleIndex).ToArray();

            return MergeArrays(MergeSort(leftHalf), MergeSort(rightHalf));
        }

        private static int[] MergeArrays(int[] left, int[] right)
        {
            var sorted = new int[left.Length + right.Length];
            var sortedInx = 0;
            var leftIndx = 0;
            var rightIndx = 0;

            while (leftIndx < left.Length && rightIndx < right.Length)
            {
                if (left[leftIndx] < right[rightIndx])
                {
                    sorted[sortedInx++] = left[leftIndx++];
                }
                else
                {
                    sorted[sortedInx++] = right[rightIndx++];
                }
            }

            while (leftIndx < left.Length)
            {
                sorted[sortedInx++] = left[leftIndx++];
            }

            while (rightIndx < right.Length)
            {
                sorted[sortedInx++] = right[rightIndx++];
            }

            return sorted;
        }
    }
}
