using System;
using System.Collections.Generic;
using System.IO;

namespace Animals
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            List<Animal> animals = new List<Animal>();
            string input = Console.ReadLine();
            
            while (input != "Beast!")
            {
                string[] animalInfo = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                try
                {
                    if (animalInfo.Length >= 2)
                    {
                        string name = animalInfo[0];
                        int age = int.Parse(animalInfo[1]);

                        if (input == "Kitten")
                        {
                            animals.Add(new Kitten(name, age));
                        }
                        else if (input == "Tomcat")
                        {
                            animals.Add(new Tomcat(name, age));
                        }

                        if (animalInfo.Length == 3)
                        {
                            string gender = animalInfo[2];
                            switch (input)
                            {
                                case "Dog":
                                    animals.Add(new Dog(name, age, gender));
                                    break;
                                case "Cat":
                                    animals.Add(new Cat(name, age, gender));
                                    break;
                                case "Frog":
                                    animals.Add(new Frog(name, age, gender));
                                    break;
                            }
                        }
                    }
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                }

                input = Console.ReadLine();
            }

            foreach (var animal in animals)
            {
                Console.WriteLine(animal);
            }
        }
    }
}
