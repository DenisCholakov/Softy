using System;

namespace PalidromeNumber
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = string.Empty;
            while ((input = Console.ReadLine()) != "END")
            {
                int num = int.Parse(input);

                bool isPalidrome = PalidromeCheck(num);

                Console.WriteLine(isPalidrome.ToString().ToLower());
            }
        }

        private static bool PalidromeCheck(int num)
        {
            int rnum = RotateNum(num);

            if (rnum == num)
            {
                return true;
            }

            return false;
        }

        private static int RotateNum(int num)
        {
            int result = 0;
            while (num != 0)
            {
                result = result * 10 + (num % 10);
                num /= 10;
            }

            return result;
        }
    }
}
