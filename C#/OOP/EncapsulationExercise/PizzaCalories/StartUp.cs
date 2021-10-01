using System;
using System.IO;

namespace PizzaCalories
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            try
            {
                string[] inputData = Console.ReadLine().Split();
                string pizzaName = inputData[1];
                Pizza pizza = new Pizza(pizzaName);

                inputData = Console.ReadLine().Split();
                string flourType = inputData[1];
                string bakingTechnique = inputData[2];
                double pizzaWeight = double.Parse(inputData[3]);
                var dough = new Dough(flourType, bakingTechnique, pizzaWeight);
                pizza.Dough = dough;

                string command;
                while ((command = Console.ReadLine()) != "END")
                {
                    inputData = command.Split();
                    string type = inputData[1];
                    double weight = double.Parse(inputData[2]);
                    var topping = new Topping(type, weight);
                    pizza.AddTopping(topping);
                }

                Console.WriteLine(pizza);

            }
            catch (ArgumentException ae)
            {
                Console.WriteLine(ae.Message);
            }
            catch(InvalidOperationException ioe)
            {
                Console.WriteLine(ioe.Message);
            }
            
        }
    }
}
