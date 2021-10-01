using System;

using Vehicles.Core.Interfaces;
using Vehicles.Core;

namespace Vehicles
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            IEngine engine = new Engine();
            engine.Run();
        }
    }
}
