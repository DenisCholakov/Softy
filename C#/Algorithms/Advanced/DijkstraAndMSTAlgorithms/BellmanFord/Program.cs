using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BellmanFord
{
    public class Edge
    {
        public int From { get; set; }
        public int To { get; set; }
        public int Weight { get; set; }
    }

    class Program
    {
        private static List<Edge> edges;

        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            int e = int.Parse(Console.ReadLine());

            edges = ReadEdges(e);

            int source = int.Parse(Console.ReadLine());
            int destination = int.Parse(Console.ReadLine());

            var distances = new double[n + 1];
            Array.Fill(distances, double.PositiveInfinity);
            distances[source] = 0;

            var prev = new int[n + 1];
            Array.Fill(prev, -1);

            for (int i = 0; i < n; i++)
            {
                bool updated = false;

                foreach (var edge in edges)
                {
                    if (double.IsPositiveInfinity(edge.From))
                    {
                        continue;
                    }

                    var newDistance = distances[edge.From] + edge.Weight;
                    if (newDistance < distances[edge.To])
                    {
                        distances[edge.To] = newDistance;
                        prev[edge.To] = edge.From;
                        updated = true;
                    }
                }

                if (!updated)
                {
                    break;
                }
            }

            foreach (var edge in edges)
            {
                if (double.IsPositiveInfinity(edge.From))
                {
                    continue;
                }

                var newDistance = distances[edge.From] + edge.Weight;

                if (newDistance < distances[edge.To])
                {
                    distances[edge.To] = newDistance;
                    Console.WriteLine("Negative Cycle Detected");
                    return;
                }
            }

            Console.WriteLine(String.Join(' ', ReconstructPath(prev, destination)));
            Console.WriteLine(distances[destination]);
        }

        private static Stack<int> ReconstructPath(int[] prev, int node)
        {
            var path = new Stack<int>();

            while (node != -1)
            {
                path.Push(node);
                node = prev[node];
            }

            return path;
        }

        private static List<Edge> ReadEdges(int e)
        {
            var result = new List<Edge>();

            for (int i = 0; i < e; i++)
            {
                var edgeInfo = Console.ReadLine().Split().Select(int.Parse).ToArray();

                int from = edgeInfo[0];
                int to = edgeInfo[1];
                int weight = edgeInfo[2];

                result.Add(new Edge
                {
                    From = from,
                    To = to,
                    Weight = weight
                });
            }

            return result;
        }
    }
}
