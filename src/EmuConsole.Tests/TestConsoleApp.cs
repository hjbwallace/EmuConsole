using System;
using System.Collections.Generic;

namespace EmuConsole.Tests
{
    public class TestConsoleApp : ConsoleApp
    {
        private readonly List<ConsoleCommand> _commands = new List<ConsoleCommand>();

        public TestConsoleApp(IConsole console) : base(console)
        {
        }

        public TestConsoleApp(IConsole console, ConsoleOptions options) : base(console, options)
        {
        }

        public TestConsoleApp WithCommand(string key, Action action)
        {
            _commands.Add(new ConsoleCommand(key, $"Command for {key}", action));
            return this;
        }

        protected override IEnumerable<ConsoleCommand> GetCommands() => _commands;
    }
}