using System;

using Raiding.Core;
using Raiding.Core.Interfaces;

namespace Raiding
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
