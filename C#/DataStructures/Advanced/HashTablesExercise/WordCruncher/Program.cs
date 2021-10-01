using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WordCruncher
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = Console.ReadLine().Split(", ", StringSplitOptions.RemoveEmptyEntries);
            string word = Console.ReadLine();

            WordCruncher wc = new WordCruncher(input, word);

            foreach (var path in wc.GetPaths())
            {
                Console.WriteLine(String.Join(' ', path));
            }
        }
    }

    public class WordCruncher
    {
        private List<Node> permutations = new List<Node>();

        public WordCruncher(string[] input, string targetWord)
        {
            permutations = GeneratePermutations(input.OrderBy(s => s), targetWord);
        }

        private List<Node> GeneratePermutations(IEnumerable<string> input, string targetWord)
        {
            if (String.IsNullOrEmpty(targetWord) || input.Count() == 0)
            {
                return null;
            }

            List<Node> returnValues = null;

            foreach (var key in input)
            {
                if (targetWord.StartsWith(key))
                {
                    if (returnValues == null)
                    {
                        returnValues = new List<Node>();
                    }

                    var node = new Node()
                    {
                        Key = key,
                        Value = GeneratePermutations(input.Except(new string[] { key }),
                            targetWord.Substring(key.Length))
                    };

                    if (node.Value == null && node.Key != targetWord)
                    {
                        continue;
                    }

                    returnValues.Add(node);
                }
            }

            return returnValues;
        }

        public IEnumerable<IEnumerable<string>> GetPaths()
        {
            List<string> way = new List<string>();

            foreach (var key in VisitPath(permutations, new List<string>()))
            {
                if (key == null)
                {
                    yield return way;
                    way.Clear();
                }
                else
                {
                    way.Add(key);
                }
            }
        }

        private IEnumerable<string> VisitPath(List<Node> permutations, List<string> path)
        {
            if (permutations == null)
            {
                foreach (var pathItem in path)
                {
                    yield return pathItem;
                }

                yield return null;
            }
            else
            {
                foreach (Node node in permutations)
                {
                    path.Add(node.Key);

                    foreach (var item in VisitPath(node.Value, path))
                    {
                        yield return item;
                    }

                    path.RemoveAt(path.Count - 1);
                }
            }
        }
    }

    public class Node
    {
        public string Key { get; set; }
        public List<Node> Value { get; set; }
    }
}
