using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace WordCount
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, int> occurances = new Dictionary<string, int>();
            string[] words = File.ReadAllLines("../../../words.txt");

            foreach (var word in words)
            {
                occurances.Add(word, NumberOfOccurances(word, "../../../text.txt"));
            }

            WriteOutput("../../../actualResult.txt", occurances);
            WriteOutput("../../../expectedResult.txt", occurances.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value));

        }

        private static int NumberOfOccurances(string word, string path)
        {
            int occurances = 0;

            using (StreamReader reader = new StreamReader(path))
            {
                string line = reader.ReadLine();

                while (line != null)
                {
                    int index = line.IndexOf(word);

                    while (index != -1)
                    {
                        occurances++;
                        index = line.IndexOf(word, index + 1);
                    }

                    line = reader.ReadLine();
                }
            }

            return occurances;
        }

        private static void WriteOutput(string path, Dictionary<string, int> occurances)
        {
            using (StreamWriter writer = new StreamWriter(path))
            {
                foreach (var pair in occurances)
                {
                    writer.WriteLine($"{pair.Key} - {pair.Value}");
                }
            }
        }
    }
}
