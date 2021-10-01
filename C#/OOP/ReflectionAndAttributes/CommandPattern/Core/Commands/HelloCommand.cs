using System;
using System.Collections.Generic;
using System.Text;

using CommandPattern.Core.Contracts;

namespace CommandPattern.Core.Commands
{
    class HelloCommand : ICommand
    {
        public string Execute(string[] args)
        {
            return $"Hello, {args[0]}";
        }
    }
}
