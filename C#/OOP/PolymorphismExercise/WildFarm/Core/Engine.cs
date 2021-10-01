using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using WildFarm.Core.Interfaces;
using WildFarm.Factories;
using WildFarm.Models.Animals;
using WildFarm.Models.Foods;

namespace WildFarm.Core
{
    public class Engine : IEngine
    {
        private ICollection<Animal> _animals;

        private readonly AnimalFactory animalFactory;
        private readonly FoodFactory foodFactory;

        public Engine()
        {
            this._animals = new HashSet<Animal>();
            animalFactory = new AnimalFactory();
            foodFactory = new FoodFactory();
        }

        public void Run()
        {
            string command;

            while ((command = Console.ReadLine()) != "End")
            {
                string[] commandArgs = command.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                string type = commandArgs[0];
                string name = commandArgs[1];
                double weight = double.Parse(commandArgs[2]);
                string[] args = commandArgs.Skip(3).ToArray();

                Animal animal = null;

                try
                {
                    animal = this.animalFactory.Create(type, name, weight, args);
                    this._animals.Add(animal);
                    Console.WriteLine(animal.ProduceSound());
                }
                catch (InvalidOperationException ioe)
                {
                    Console.WriteLine(ioe.Message);
                }

                string[] foodArgs = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);

                string foodType = foodArgs[0];
                int foodQty = int.Parse(foodArgs[1]);

                try
                {
                    Food food = this.foodFactory.CreateFood(foodType, foodQty);

                    animal?.Feed(food);
                }
                catch (InvalidOperationException ioe)
                {
                    Console.WriteLine(ioe.Message);
                }
            }

            foreach (var animal in this._animals)
            {
                Console.WriteLine(animal);
            }
        }
    }
}
