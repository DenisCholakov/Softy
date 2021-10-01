using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

namespace EmergencyPlan
{
    public class Edge
    {
        public int First { get; set; }
        public int Second { get; set; }
        public double Distance { get; set; }

        public TimeSpan Minutes { get { return TimeSpan.FromMinutes(this.Distance); } }
    }

    class Program
    {
        private static List<Edge>[] graph;
        private static List<int> exitRooms;
        static void Main(string[] args)
        {
            int roomsCount = int.Parse(Console.ReadLine());
            exitRooms = Console.ReadLine().Split().Select(int.Parse).ToList();
            int connectionsCount = int.Parse(Console.ReadLine());

            graph = ReadGraph(connectionsCount, roomsCount);

            var maxEvacTime = TimeSpan.ParseExact(Console.ReadLine(), @"mm\:ss", null);

            var distances = new double[graph.Length];

            for (int i = 0; i < graph.Length; i++)
            {
                distances[i] = double.PositiveInfinity;
            }

            for (int i = 0; i < graph.Length; i++)
            {
                if (!exitRooms.Contains(i))
                {
                    distances[i] = Dijkstra(i, exitRooms);
                }
            }

            for (int i = 0; i < roomsCount; i++)
            {
                if (!exitRooms.Contains(i))
                {
                    if (double.IsPositiveInfinity(distances[i]))
                    {
                        Console.WriteLine($"Unreachable {i} (N/A)");
                    }
                    else if (maxEvacTime.TotalMinutes < distances[i])
                    {
                        Console.WriteLine($"Unsafe {i} ({TimeSpan.FromMinutes(distances[i])})");
                    }
                    else
                    {
                        Console.WriteLine($"Safe {i} ({TimeSpan.FromMinutes(distances[i])})");
                    }
                    
                }
            }
        }

        private static double Dijkstra(int source, List<int> exitRooms)
        {
            var minDistance = double.PositiveInfinity;

            var distances = new double[graph.Length];

            for (int i = 0; i < graph.Length; i++)
            {
                distances[i] = double.PositiveInfinity;
            }

            distances[source] = 0;

            var queue = new OrderedBag<int>(Comparer<int>.Create((f, s) => distances[f].CompareTo(distances[s])));
            queue.Add(source);

            while (queue.Count > 0)
            {
                var node = queue.RemoveFirst();

                if (exitRooms.Contains(node))
                {
                    if (distances[node] < minDistance)
                    {
                        minDistance = distances[node];
                    }
                }

                var children = graph[node];

                foreach (var edge in children)
                {
                    var childNode = edge.First == node ? edge.Second : edge.First;

                    if (double.IsPositiveInfinity(distances[childNode]))
                    {
                        queue.Add(childNode);
                    }

                    var newDistance = distances[node] + edge.Distance;

                    if (newDistance < distances[childNode])
                    {
                        distances[childNode] = newDistance;

                        queue = new OrderedBag<int>(queue, Comparer<int>.Create((f, s) => distances[f].CompareTo(distances[s])));
                    }
                }
            }

            return minDistance;
        }

        private static List<Edge>[] ReadGraph(int connectionsCount, int roomsCount)
        {
            var result = new List<Edge>[roomsCount];

            for (int i = 0; i < roomsCount; i++)
            {
                result[i] = new List<Edge>();
            }

            for (int i = 0; i < connectionsCount; i++)
            {
                var connection = Console.ReadLine().Split();
                var first = int.Parse(connection[0]);
                var second = int.Parse(connection[1]);
                var minutes = TimeSpan.ParseExact(connection[2], @"mm\:ss", null);
                var distance = minutes.TotalMinutes;

                var edge = new Edge
                {
                    First = first,
                    Second = second,
                    Distance = distance
                };

                result[first].Add(edge);
                result[second].Add(edge);
            }

            return result;
        }
    }
}
