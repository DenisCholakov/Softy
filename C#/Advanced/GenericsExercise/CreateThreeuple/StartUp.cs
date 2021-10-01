using System;
using System.IO;

namespace CreateThreeuple
{
    class StartUp
    {
        static void Main(string[] args)
        {
            string[] firstInput = Console.ReadLine().Split();
            string fullName = firstInput[0] + " " + firstInput[1];
            string adress = firstInput[2];
            string town = firstInput[3];
            for (int i = 4; i < firstInput.Length; i++)
            {
                town += (" " + firstInput[i]);
            }
            Threeuple<string, string, string> firstTuple = new Threeuple<string, string, string>(fullName, adress, town);

            string[] secondInput = Console.ReadLine().Split();
            string name = secondInput[0];
            int litersOfBeer = int.Parse(secondInput[1]);
            bool isDrunk = secondInput[2] == "drunk";
            Threeuple<string, int, bool> secondTuple = new Threeuple<string, int, bool>(name, litersOfBeer, isDrunk);

            string[] thirdInput = Console.ReadLine().Split();
            string accountHolder= thirdInput[0];
            double accountBalance = double.Parse(thirdInput[1]);
            string bankName = thirdInput[2];
            Threeuple<string, double, string> thirdTuple = new Threeuple<string, double, string>(accountHolder, accountBalance, bankName);

            Console.WriteLine(firstTuple);
            Console.WriteLine(secondTuple);
            Console.WriteLine(thirdTuple);
        }
    }
}
