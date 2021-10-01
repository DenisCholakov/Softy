using System;
using System.Collections.Generic;
using System.IO;

namespace Salaries
{
    class Program
    {
        private static List<int>[] graph;
        private static Dictionary<int, int> salaries;

        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            graph = ReadGraph(n);
            salaries = new Dictionary<int, int>();
            var salarySum = 0;

            for (int i = 0; i < graph.Length; i++)
            {
                var salary = GetSalary(i);
                salarySum += salary;
            }

            Console.WriteLine(salarySum);
        }

        private static int GetSalary(int node)
        {
            var children = graph[node];

            if (salaries.ContainsKey(node))
            {
                return salaries[node];
            }

            if (children.Count == 0)
            {
                return 1;
            }

            var salary = 0;

            foreach (var child in children)
            {
                salary += GetSalary(child);
            }

            salaries.Add(i, salary);
            return salary;
        }

        private static List<int>[] ReadGraph(int n)
        {
            var result = new List<int>[n];

            for (int i = 0; i < n; i++)
            {
                string row = Console.ReadLine();
                result[i] = new List<int>();

                for (int j = 0; j < n; j++)
                {
                    if (row[j] == 'Y')
                    {
                        result[i].Add(j);
                    }
                }
            }

            return result;
        }
    }
}
