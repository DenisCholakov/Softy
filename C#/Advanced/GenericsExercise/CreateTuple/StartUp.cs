using System;
using System.IO;

namespace CreateTuple
{
    class StartUp
    {
        static void Main(string[] args)
        {
            string[] firstInput = Console.ReadLine().Split();
            string fullName = firstInput[0] + " " + firstInput[1];
            string adress = firstInput[2];
            Tuple<string, string> firstTuple = new Tuple<string, string>(fullName, adress);

            string[] secondInput = Console.ReadLine().Split();
            string name = secondInput[0];
            int litersOfBeer = int.Parse(secondInput[1]);
            Tuple<string, int> secondTuple = new Tuple<string, int>(name, litersOfBeer);
            
            string[] thirdInput = Console.ReadLine().Split();
            int number1 = int.Parse(thirdInput[0]);
            double number2 = double.Parse(thirdInput[1]);
            Tuple<int, double> thirdTuple = new Tuple<int, double>(number1, number2);

            Console.WriteLine(firstTuple);
            Console.WriteLine(secondTuple);
            Console.WriteLine(thirdTuple);
        }
    }
}
