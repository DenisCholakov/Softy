using System;
using System.Collections.Generic;
using System.Text;

namespace RawData
{
    public class Cargo
    {
        public Cargo(int wight, string type)
        {
            Wight = wight;
            Type = type;
        }

        public int Wight { get; set; }
        public string Type { get; set; }
    }
}
