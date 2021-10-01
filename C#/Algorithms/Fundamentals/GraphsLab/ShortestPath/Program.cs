using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ShortestPath
{
    class Program
    {
        private static List<int>[] graph;
        private static bool[] visited;
        private static int[] parent;
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            int e = int.Parse(Console.ReadLine());
            visited = new bool[n + 1];
            parent = new int[n + 1];
            Array.Fill<int>(parent, -1);

            graph = ReadGraph(n, e);

            int source = int.Parse(Console.ReadLine());
            int destination = int.Parse(Console.ReadLine());

            FindPathBfs(source, destination);
        }

        private static void FindPathBfs(int startNode, int destination)
        {
            if (visited[startNode])
            {
                return;
            }

            visited[startNode] = true;

            var queue = new Queue<int>();
            queue.Enqueue(startNode);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();

                if (node == destination)
                {
                    var path = ReconstructPath(destination);
                    Console.WriteLine($"Shortest path length is: {path.Count - 1}");
                    Console.WriteLine(String.Join(' ', path));

                    return;
                }

                foreach (var child in graph[node])
                {
                    if (!visited[child])
                    {
                        parent[child] = node;
                        queue.Enqueue(child);
                        visited[child] = true;
                    }
                }
            }
        }

        private static Stack<int> ReconstructPath(int destination)
        {
            var path = new Stack<int>();

            int index = destination;

            while (index != -1)
            {
                path.Push(index);
                index = parent[index];
            }

            return path;
        }

        private static List<int>[] ReadGraph(int n, int e)
        {
            List<int>[] result = new List<int>[n + 1];

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = new List<int>();
            }

            for (int i = 0; i < e; i++)
            {
                var edge = Console.ReadLine().Split().Select(int.Parse).ToArray();
                var from = edge[0];
                var to = edge[1];

                result[from].Add(to);
            }

            return result;
        }
    }
}
