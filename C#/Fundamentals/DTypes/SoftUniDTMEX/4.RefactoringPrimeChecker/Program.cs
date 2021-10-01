using System;

namespace _4.RefactoringPrimeChecker
{
    class Program
    {
        static void Main(string[] args)
        {
            //int ___Do___ = int.Parse(Console.ReadLine());
            //for (int takoa = 2; takoa <= ___Do___; takoa++)
            //{
            //    bool takovalie = true;
            //    for (int cepitel = 2; cepitel < takoa; cepitel++)
            //    {
            //        if (takoa % cepitel == 0)
            //        {
            //            takovalie = false;
            //            break;
            //        }
            //    }
            //    Console.WriteLine("{0} -> {1}", takoa, takovalie);
            //}

            int num = int.Parse(Console.ReadLine());
            for (int i = 2; i <= num; i++)
            {
                bool isPrime = true;
                for (int divider = 2; divider < i; divider++)
                {
                    if (i % divider == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }
                Console.WriteLine("{0} -> {1}", i, isPrime.ToString().ToLower());
            }
        }
    }
}
