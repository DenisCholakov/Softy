using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Boxes
{
    public class Box : IComparable<Box>
    {
        public int Width { get; set; }
        public int Depth { get; set; }
        public int Height { get; set; }

        public override string ToString() => $"{this.Width} {this.Depth} {this.Height}";

        public int CompareTo([AllowNull] Box other)
        {
            // TODO: to be refactored

            int result = other.Width - this.Width;

            if (result < 0)
            {
                result = other.Depth - this.Depth;

                if (result < 0)
                {
                    return other.Height - this.Height;
                }
            }

            return result;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            int boxesCount = int.Parse(Console.ReadLine());
            var boxes = new Box[boxesCount];

            var height = new int[boxesCount];
            var prev = new int[boxesCount];
            Array.Fill(prev, -1);

            for (int i = 0; i < boxesCount; i++)
            {
                var boxData = Console.ReadLine().Split().Select(int.Parse).ToArray();

                var box = new Box
                {
                    Width = boxData[0],
                    Depth = boxData[1],
                    Height = boxData[2]
                };

                boxes[i] = box;
            }

            int maxHeight = 0;
            int lastIndex = -1;

            for (int i = 0; i < boxes.Length; i++)
            {
                height[i] = 0;
                prev[i] = -1;

                for (int j = 0; j < i; j++)
                {
                    if ((boxes[i].CompareTo(boxes[j]) < 0) && (height[j] + boxes[i].Height) > height[i])
                    {
                        height[i] = height[j] + boxes[i].Height;
                        prev[i] = j;
                    }
                }

                if (height[i] > maxHeight)
                {
                    maxHeight = height[i];
                    lastIndex = i;
                }
            }

            var stack = new Stack<int>();

            while (lastIndex != -1)
            {
                stack.Push(lastIndex);
                lastIndex = prev[lastIndex];
            }

            while (stack.Count > 0)
            {
                var index = stack.Pop();
                Console.WriteLine(boxes[index]);
            }
        }
    }
}
