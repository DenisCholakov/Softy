using System;
using System.Collections.Generic;
using System.Text;

namespace BorderControl
{
    interface IBuyer
    {
        public string Name { get; }
        public int BuyFood();
    }
}
