using System;

namespace _10.RageExpenses
{
    class Program
    {
        static void Main(string[] args)
        {
            int lostGames = int.Parse(Console.ReadLine());
            double headsetPrice = double.Parse(Console.ReadLine());
            double mousePrice = double.Parse(Console.ReadLine());
            double keyboardPrice = double.Parse(Console.ReadLine());
            double displayPrice = double.Parse(Console.ReadLine());

            int headsetsBroken = lostGames / 2;
            int mousesBroken = lostGames / 3;
            int keyboardsBroken = lostGames / 6;
            int displaysBroken = lostGames / 12;

            double expenses = headsetsBroken * headsetPrice + mousesBroken * mousePrice + keyboardsBroken * keyboardPrice + displaysBroken * displayPrice;

            Console.WriteLine($"Rage expenses: {expenses:f2} lv.");
        }
    }
}
