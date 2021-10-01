using System;
using System.Collections.Generic;
using System.Text;

namespace CreateTuple
{
    class Tuple<TFirst, TSecond>
    {
        public Tuple(TFirst value1, TSecond value2)
        {
            this.Value1 = value1;
            this.Value2 = value2;
        }

        public TFirst Value1 { get; set; }
        public TSecond Value2 { get; set; }

        public override string ToString() => $"{this.Value1} -> {this.Value2}";
    }
}
