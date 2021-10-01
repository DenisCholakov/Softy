using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace KaminoFactory
{
    class Program
    {
        static void Main(string[] args)
        {
            int SeqLength = int.Parse(Console.ReadLine());
            int[] bestSample = new int[SeqLength];

            string input = Console.ReadLine();
            int longestSubseq = 0;
            int seqIndex = 0;
            int longestSum = 0;
            int bestSampleNum = 0;
            int sample = 0;
            while (input != "Clone them!")
            {
                int sampleSum = 0;
                int seq = 1;
                int sampleSeq = 0;
                int sampleIndex = 0;
                sample++;
                int[] sequence = input.Split('!', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                for (int i = 0; i < SeqLength; i++)
                {
                    if (sequence[i] == 1)
                    {
                        sampleSum++;
                        if (i != 0 && sequence[i-1] == 1)
                        {
                            seq++;
                        }
                    }
                    else
                    {
                        seq = 1;
                    }

                    if (sampleSeq < seq)
                    {
                        sampleSeq = seq;
                        sampleIndex = i + 1 - seq;
                    }
                }

                if (longestSubseq <= sampleSeq)
                {
                    if (longestSubseq < sampleSeq)
                    {
                        longestSubseq = sampleSeq;
                        seqIndex = sampleIndex;
                        longestSum = sampleSum;
                        bestSampleNum = sample;
                        bestSample = sequence;
                    }
                    else if (longestSubseq == sampleSeq && seqIndex >= sampleIndex)
                    {
                        if (seqIndex > sampleIndex)
                        {
                            seqIndex = sampleIndex;
                            longestSum = sampleSum;
                            bestSampleNum = sample;
                            bestSample = sequence;
                        }
                        else if (seqIndex == sampleIndex && longestSum < sampleSum)
                        {
                            longestSum = sampleSum;
                            bestSampleNum = sample;
                            bestSample = sequence;
                        }
                    }
                }
                input = Console.ReadLine();
            }

            Console.WriteLine($"Best DNA sample {bestSampleNum} with sum: {longestSum}.");
            Console.WriteLine(String.Join(' ', bestSample));
        }
    }
}
