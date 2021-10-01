using System;

namespace _2.LeftOrRight
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {
                string[] numbers = Console.ReadLine().Split(" ", 2);

                long num1 = long.Parse(numbers[0]);
                long num2 = long.Parse(numbers[1]);
                long maxNum = num1 > num2 ? num1 : num2;

                Console.WriteLine(SumOfDigits(maxNum));
            }
            

            

            
        }

        static int SumOfDigits(long num)
        {
            //long sum = 0;
            //while (num != 0)
            //{
            //    sum += num % 10;
            //    num /= 10;
            //}

            int sum = 0;
            string strNum = num.ToString();
            strNum = strNum.Trim('-');
            for (int i = 0; i < strNum.Length; i++)
            {
                int n = int.Parse(strNum[i].ToString());
                sum += n;
            }

            return sum;
        }
    }
}
