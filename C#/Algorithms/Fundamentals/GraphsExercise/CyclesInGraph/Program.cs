using System;
using System.Collections.Generic;
using System.IO;

namespace CyclesInGraph
{
    class Program
    {
        private static Dictionary<string, List<string>> graph;
        private static HashSet<string> visited;
        private static HashSet<string> cycles;
        private static bool isAcyclic;

        static void Main(string[] args)
        {
            graph = ReadGraph();
            visited = new HashSet<string>();
            cycles = new HashSet<string>();
            isAcyclic = true;

            foreach (var node in graph)
            {
                if (visited.Contains(node.Key))
                {
                    continue;
                }

                Dfs(node.Key);
            }

            if (isAcyclic)
            {
                Console.WriteLine("Acyclic: Yes");
            }
            else
            {
                Console.WriteLine("Acyclic: No");
            }
        }

        private static void Dfs(string node)
        {
            if (cycles.Contains(node))
            {
                isAcyclic = false;
                return;
            }

            if (visited.Contains(node))
            {
                return;
            }

            visited.Add(node);
            cycles.Add(node);

            foreach (var child in graph[node])
            {
                Dfs(child);
            }

            cycles.Remove(node);
        }

        private static Dictionary<string, List<string>> ReadGraph()
        {
            var result = new Dictionary<string, List<string>>();

            string input = Console.ReadLine();

            while (input != "End")
            {
                var inputArgs = input.Split('-');
                string parent = inputArgs[0];
                string child = inputArgs[1];

                if (!result.ContainsKey(parent))
                {
                    result.Add(parent, new List<string>());
                }

                if (!result.ContainsKey(child))
                {
                    result.Add(child, new List<string>());
                }

                result[parent].Add(child);

                input = Console.ReadLine();
            }

            return result;
        }
    }
}
