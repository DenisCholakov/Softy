using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Telephony
{
    class Smartphone : ICallable, IBrowsable
    {
        public string Browse(string url)
        {
            if (url.Any(x => Char.IsDigit(x)))
            {
                throw new ArgumentException("Invalid URL!");
            }

            return $"Browsing: {url}!";
        }

        public string Call(string number)
        {
            if (!number.All(x => Char.IsDigit(x)))
            {
                throw new ArgumentException("Invalid number!");
            }

            return $"Calling... {number}";
        }
    }
}
