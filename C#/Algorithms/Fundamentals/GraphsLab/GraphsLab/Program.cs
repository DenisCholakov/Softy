using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GraphsLab
{
    class Program
    {
        private static List<int>[] graph;
        private static bool[] visited;
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            graph = new List<int>[n];
            visited = new bool[n];

            for (int i = 0; i < n; i++)
            {
                string input = Console.ReadLine();

                if (String.IsNullOrEmpty(input))
                {
                    graph[i] = new List<int>();
                }
                else
                {
                    graph[i] = input.Split().Select(int.Parse).ToList();
                }
            }

            for (int node = 0; node < n; node++)
            {
                if (!visited[node])
                {
                    var component = new List<int>();
                    Dfs(node, component);
                    Console.WriteLine($"Connected component: {String.Join(' ', component)}");
                }
            }
        }

        private static void Dfs(int node, List<int> component)
        {
            if (visited[node])
            {
                return;
            }

            visited[node] = true;

            foreach (var child in graph[node])
            {
                Dfs(child, component);
            }

            component.Add(node);
        }
    }
}
