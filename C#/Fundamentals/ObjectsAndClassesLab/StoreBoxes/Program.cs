using System;
using System.Collections.Generic;
using System.Linq;

namespace StoreBoxes
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Box> boxes = new List<Box>();

            string[] input = Console.ReadLine().Split().ToArray();
            while (input[0] != "end")
            {
                Box box = new Box();
                box.Item = new Item();

                box.SerialNumber = input[0];
                box.Item.Name = input[1];
                box.ItemQuality = int.Parse(input[2]);
                box.Item.Price = decimal.Parse(input[3]);
                box.PriceForABox = box.Item.Price * box.ItemQuality;
                
                boxes.Add(box);

                input = Console.ReadLine().Split().ToArray();
            }

            boxes = boxes.OrderByDescending(x => x.PriceForABox).ToList();

            foreach (var box in boxes)
            {
                Console.WriteLine(box.SerialNumber);
                Console.WriteLine($"-- {box.Item.Name} - ${box.Item.Price:f2}: {box.ItemQuality}");
                Console.WriteLine($"-- ${box.PriceForABox:f2}");
            }
        }
    }

    public class Box
    {
        public string SerialNumber { get; set; }
        public Item Item { get; set; }
        public int ItemQuality { get; set; }
        public decimal PriceForABox { get; set; }
    }

    public class Item
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
