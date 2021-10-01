using System;
using System.Collections.Generic;
using System.Text;

namespace CreateThreeuple
{
    class Threeuple <T1, T2, T3> where T3 : IComparable
    {
        public Threeuple(T1 value1, T2 value2, T3 value3)
        {
            Value1 = value1;
            Value2 = value2;
            Value3 = value3;
        }

        public T1 Value1 { get; set; }
        public T2 Value2 { get; set; }
        public T3 Value3 { get; set; }

        public override string ToString() => $"{Value1} -> {Value2} -> {Value3}";
    }
}
