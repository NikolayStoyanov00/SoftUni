using System;
using System.Collections.Generic;
using System.Text;

namespace CommandPattern.Core.Contracts
{
    public class Engine : IEngine
    {
        public Engine(ICommandInterpreter commandInterpreter)
        {
            this.commandInterpreter = commandInterpreter;
        }

        private readonly ICommandInterpreter commandInterpreter;

        public void Run()
        {
            while (true)
            {
                var command = Console.ReadLine();
                commandInterpreter.Read(command);
            }
        }
    }
}
