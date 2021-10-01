using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace EvenTimes
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            Dictionary<int, int> occurances = new Dictionary<int, int>();

            for (int i = 0; i < n; i++)
            {
                int currentNum = int.Parse(Console.ReadLine());

                if (!occurances.ContainsKey(currentNum))
                {
                    occurances.Add(currentNum, 0);
                }

                occurances[currentNum]++;
            }

            // var num = occurances.Where(x => x.Value % 2 == 0).FirstOrDefault().Key;
            // Console.WriteLine(num);

            foreach (var pair in occurances)
            {
                if (pair.Value % 2 == 0)
                {
                    Console.WriteLine(pair.Key);
                    break;
                }
            }
        }
    }
}
