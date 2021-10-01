using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealer.DTO
{
    public class PartsInutModel
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public int supplierId { get; set; }
    }
}
