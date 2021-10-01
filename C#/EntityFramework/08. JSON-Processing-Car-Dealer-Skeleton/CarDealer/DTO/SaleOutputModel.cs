using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealer.DTO
{
    public class SaleOutputModel
    {
        public CarOutputModel car { get; set; }

        public string customerName { get; set; }

        public string Discount { get; set; }

        public string price { get; set; }

        public string priceWithDiscount { get; set; }

    }

    public class CarOutputModel
    {
        public string Make { get; set; }

        public string Model { get; set; }

        public long TravelledDistance { get; set; }
    }
}
