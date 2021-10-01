using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Telephony
{
    class StationaryPhone : ICallable
    {
        public string Call(string number)
        {
            if (!number.All(x => Char.IsDigit(x)))
            {
                throw new ArgumentException("Invalid number!");
            }

            return $"Dialing... {number}";
        }
    }
}
