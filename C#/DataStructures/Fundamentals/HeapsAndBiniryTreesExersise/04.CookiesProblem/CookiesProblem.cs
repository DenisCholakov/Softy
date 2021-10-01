using System;
using Wintellect.PowerCollections;


namespace _04.CookiesProblem
{
    public class CookiesProblem
    {
        public int Solve(int k, int[] cookies)
        {
            MinHeap<int> queue = new MinHeap<int>();
            
            int operationsNeeded = 0;

            foreach (var cookie in cookies)
            {
                queue.Add(cookie);
            }

            while (queue.Peek() < k && queue.Size > 1)
            {
                int leastSweetCookie = queue.Dequeue();
                int secondLeastSweetCookie = queue.Dequeue();
                int newCookie = leastSweetCookie + 2 * secondLeastSweetCookie;
                queue.Add(newCookie);
                operationsNeeded++;
            }

            return queue.Peek() < k ? -1 : operationsNeeded;
        }

        public int SolveWithOrderedBag (int k, int[] cookies)
        {
            OrderedBag<int> queue = new OrderedBag<int>();
            int numOfOperations = 0;

            foreach (var cookie in cookies)
            {
                queue.Add(cookie);
            }

            while (queue.GetFirst() < k && queue.Count > 1)
            {
                int leastSweet = queue.RemoveFirst();
                int secondLeastSweet = queue.RemoveFirst();
                int newSweet = leastSweet + 2 * secondLeastSweet;
                queue.Add(newSweet);
                numOfOperations++;
            }

            return queue.GetFirst() < k ? -1 : numOfOperations;
        }
    }
}
