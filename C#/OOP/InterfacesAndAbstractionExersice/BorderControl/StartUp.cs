using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace BorderControl
{
     public class StartUp
    {
        static void Main(string[] args)
        {
            HashSet<IBuyer> buyers = new HashSet<IBuyer>();
            int n = int.Parse(Console.ReadLine());
            string input;
            int foodBought = 0;

            for (int i = 0; i < n; i++)
            {
                string[] info = Console.ReadLine().Split();
                string name = info[0];
                int age = int.Parse(info[1]);

                if (info.Length == 3)
                {
                    string group = info[2];
                    buyers.Add(new Rebel(name, age, group));
                }
                else
                {
                    string id = info[2];
                    DateTime birthdate = DateTime.Parse(info[3], new CultureInfo("ar-BH"));
                    buyers.Add(new Citizen(name, age, id, birthdate));
                }
            }

            while ((input = Console.ReadLine()) != "End")
            {
                IBuyer buyer = buyers.FirstOrDefault(x => x.Name == input);

                if (buyer != null)
                {
                    foodBought += buyer.BuyFood();
                }
            }

            Console.WriteLine(foodBought);
        }
    }
}
