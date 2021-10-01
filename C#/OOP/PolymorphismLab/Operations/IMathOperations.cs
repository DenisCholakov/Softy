using System;
using System.Collections.Generic;
using System.Text;

namespace Operations
{
    public interface IMathOperations
    {
        public int Add(int a, int b);
        public double Add(double a, double b, double c);
        public decimal Add(decimal a, decimal b, decimal c);
    }
}
