using System;
using System.Collections.Generic;
using System.Text;

namespace CarSalesman
{
    class Car
    {
        public Car(string model, Engine engine)
        {
            Model = model;
            Engine = engine;
            Weight = "n/a";
            Color = "n/a";
        }

        public Car(string model, Engine engine, string weight) : this(model, engine)
        {
            Weight = weight;
        }

        public Car(string model, Engine engine, string weight, string color) : this(model, engine)
        {
            Weight = weight;
            Color = color;
        }

        public string Model { get; set; }
        public Engine Engine { get; set; }
        public string Weight { get; set; }
        public string Color { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{Model}:");
            sb.AppendLine($"{Engine}");
            sb.AppendLine($"  Weight: {Weight}");
            sb.AppendLine($"  Color: {Color}");
            return sb.ToString().TrimEnd();
        }
    }
}
