using System;
using System.ComponentModel.Design.Serialization;
using System.Linq;

namespace MaxSeqOfEqElements
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] arr = Console.ReadLine().Split().Select(int.Parse).ToArray();

            int index = 0;
            int maxSeq = 1;
            int seq = 1;
            for (int i = 1; i < arr.Length; i++)
            {
                if (arr[i] == arr[i-1])
                {
                    seq++;
                }
                else
                {
                    seq = 1;
                }

                if (seq > maxSeq)
                {
                    maxSeq = seq;
                    index = i + 1 - seq;
                }
            }

            for (int i = index; i < index + maxSeq; i++)
            {
                Console.Write(arr[i] + " ");
            }
        }
    }
}
