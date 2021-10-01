using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ArticulationPoints
{
    class Program
    {
        private static List<int>[] graph;
        private static int[] depths;
        private static int[] lowpoints;
        private static int[] parents;
        private static bool[] visited;
        private static List<int> articulationPoints;

        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            int l = int.Parse(Console.ReadLine());

            depths = new int[n];
            lowpoints = new int[n];
            parents = new int[n];
            visited = new bool[n];
            articulationPoints = new List<int>();

            Array.Fill(parents, -1);

            graph = ReadGraph(n, l);

            for (int node = 0; node < graph.Length; node++)
            {
                if (!visited[node])
                {
                    FindArticulationPoits(node, 1);
                }
            }

            Console.WriteLine($"Articulation points: {String.Join(", ", articulationPoints)}");
        }

        private static void FindArticulationPoits(int node, int depth)
        {
            visited[node] = true;
            lowpoints[node] = depth;
            depths[node] = depth;

            var childCount = 0;
            var isArticulationPoint = false;

            foreach (var child in graph[node])
            {
                if (!visited[child])
                {
                    parents[child] = node;
                    FindArticulationPoits(child, depth + 1);
                    childCount++;

                    if (lowpoints[child] >= depth)
                    {
                        isArticulationPoint = true;
                    }

                    lowpoints[node] = Math.Min(lowpoints[node], lowpoints[child]);
                }
                else if (parents[node] != child)
                {
                    lowpoints[node] = Math.Min(lowpoints[node], depths[child]);
                }
            }

            if((parents[node] == -1 && childCount > 1) || (parents[node] != -1 && isArticulationPoint))
            {
                articulationPoints.Add(node);
            }
        }

        private static List<int>[] ReadGraph(int n, int l)
        {
            var result = new List<int>[n];

            for (int i = 0; i < n; i++)
            {
                result[i] = new List<int>();
            }

            for (int i = 0; i < l; i++)
            {
                var numbers = Console.ReadLine().Split(", ").Select(int.Parse).ToArray();

                var first = numbers[0];

                for (int j = 1; j < numbers.Length; j++)
                {
                    var second = numbers[j];
                    result[first].Add(second);
                    result[second].Add(first);
                }
            }

            return result;
        }
    }
}
