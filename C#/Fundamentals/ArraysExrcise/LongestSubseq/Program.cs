using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace LongestSubseq
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] nums = Console.ReadLine().Split().Select(int.Parse).ToArray();

            List<int> longestSeq = new List<int>();
            for (int i = 0; i < nums.Length; i++)
            {
                List<int> seq = new List<int>() {nums[i]};
                FindLongestSeq(nums.Skip(i+1).Take(nums.Length + 1 - i).ToArray(), nums[i], seq, longestSeq);
            }

            foreach (var num in longestSeq)
            {
                Console.Write(num + " ");
            }
        }

        static void FindLongestSeq(int[] arr, int num, List<int> seq, List<int> longestSeq)
        {

            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] > num)
                {
                    seq.Add(arr[i]);
                    FindLongestSeq(arr.Skip(i+1).Take(arr.Length + 1 - i).ToArray(), arr[i], seq, longestSeq);
                }
            }

            if (seq.Count > longestSeq.Count)
            {
                longestSeq.Clear();
                foreach (var p in seq)
                {
                    longestSeq.Add(p);
                }
            }

            seq.RemoveAt(seq.Count - 1);
        }

    }
}
