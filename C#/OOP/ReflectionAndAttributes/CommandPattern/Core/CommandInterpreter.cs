using System;
using System.Linq;
using System.Reflection;
using CommandPattern.Core.Commands;
using CommandPattern.Core.Contracts;

namespace CommandPattern.Core
{
    class CommandInterpreter : ICommandInterpreter
    {
        public string Read(string args)
        {
            string result = String.Empty;

            string[] inputTokens = args.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string commandType = inputTokens[0].ToLower() + "command";
            string[] commandArgs = inputTokens.Skip(1).ToArray();

            Type type = Assembly.GetCallingAssembly().GetTypes()
                .FirstOrDefault(x => x.Name.ToLower() == commandType);

            if (type != null)
            {
                ICommand command = (ICommand)Activator.CreateInstance(type);

                result = command.Execute(commandArgs);

            }

            return result;
        }
    }
}
