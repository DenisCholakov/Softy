using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;

namespace Garden
{
    class Program
    {
        public class Flower
        {
            public Flower(int row, int col)
            {
                this.Row = row;
                this.Col = col;
            }

            public int Row { get; set; }
            public int Col { get; set; }
        }
        static void Main(string[] args)
        {
            int[] dimentions = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int n = dimentions[0];
            int m = dimentions[1];
            int[,] garden = new int[n, m];
            List<Flower> plantedFlowers = new List<Flower>();
            string input = Console.ReadLine();

            while (input != "Bloom Bloom Plow")
            {
                int[] flowerToPlant = input.Split().Select(int.Parse).ToArray();
                int flowerRow = flowerToPlant[0];
                int flowerCol = flowerToPlant[1];

                if (IndexIsValid(flowerRow, flowerCol, garden))
                {
                    plantedFlowers.Add(new Flower(flowerRow, flowerCol));
                }
                else
                {
                    Console.WriteLine("Invalid coordinates.");
                }

                input = Console.ReadLine();
            }

            foreach (var flower in plantedFlowers)
            {
                // why is square when we have two dimentions. If slow chage
                for (int col = 0; col < garden.GetLength(1); col++)
                {
                    garden[flower.Row, col]++;
                }

                for (int row = 0; row < garden.GetLength(0); row++)
                {
                    if (row != flower.Row)
                    {
                        garden[row, flower.Col]++;
                    }   
                }
            }

            Print(garden);
        }

        private static void Print(int[,] garden)
        {

            for (int row = 0; row < garden.GetLength(0); row++)
            {
                StringBuilder sb = new StringBuilder();

                for (int col = 0; col < garden.GetLength(1); col++)
                {
                    sb.Append(garden[row, col] + " ");
                }

                Console.WriteLine(sb.ToString().TrimEnd());
            }
        }

        private static bool IndexIsValid(int row, int col, int[,] matrix)
        {
            int rowsCount = matrix.GetLength(0);
            int colsCount = matrix.GetLength(1);

            if (row < 0 || row >= rowsCount || col < 0 || col >= colsCount)
            {
                return false;
            }

            return true;
        }
    }
}
