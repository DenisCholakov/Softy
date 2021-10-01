using System;
using System.Collections.Generic;
using System.Text;

namespace BoxOfT
{
    public class Box<T> where T : IComparable
    {
        public Box()
        {
            this.Values = new List<T>();
        }

        public List<T> Values { get; set; }

        public void Swap(int index1, int index2)
        {
            T temp = Values[index1];
            Values[index1] = Values[index2];
            Values[index2] = temp;
        }

        public int LargerElCount(T value)
        {
            int count = 0;

            foreach (var item in this.Values)
            {
                if (IsBigger(item, value))
                {
                    count++;
                }
            }

            return count;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var item in this.Values)
            {
                sb.AppendLine($"{item.GetType()}: {item}");
            }

            return sb.ToString().TrimEnd();
        }

        private bool IsBigger(T value1, T value2)
        {
            return value1.CompareTo(value2) > 0;
        }
    }
}
