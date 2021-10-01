using System;
using System.Collections.Generic;
using System.Text;

namespace TakeSkipRope
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = Console.ReadLine();

            int num = 0;
            List<int> nums = new List<int>();
            List<char> chars = new List<char>();

            for (int i = 0; i < text.Length; i++)
            {
                if (int.TryParse(text[i].ToString(), out num))
                {
                    nums.Add(num);
                }
                else
                {
                    chars.Add(text[i]);
                }
            }

            IList<int> takeList = new List<int>();
            IList<int> skipList = new List<int>();

            for (int i = 0; i < nums.Count; i++)
            {
                if (i % 2 == 0)
                {
                    takeList.Add(nums[i]);
                }
                else
                {
                    skipList.Add(nums[i]);
                }
            }

            StringBuilder sb = new StringBuilder();

            int startIndex = 0;
            for (int i = 0; i < takeList.Count; i++)
            {
                for (int j = startIndex; j < takeList[i] + startIndex; j++)
                {
                    if (j < chars.Count)
                    {
                        sb.Append(chars[j]);
                    }
                }
                startIndex += skipList[i] + takeList[i];
            }

            Console.WriteLine(sb);
        }
    }
}
