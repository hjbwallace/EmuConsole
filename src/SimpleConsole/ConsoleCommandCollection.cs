using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleConsole
{
    public class ConsoleCommandCollection
    {
        private readonly List<ConsoleCommand> _commands;
        private readonly List<string> _keys;

        public ConsoleCommandCollection(params ConsoleCommand[] commands)
            : this((IEnumerable<ConsoleCommand>)commands)
        {
        }

        public ConsoleCommandCollection(IEnumerable<ConsoleCommand> commands)
        {
            _commands = new List<ConsoleCommand>();
            _keys = new List<string>();

            if (commands?.Any() != true)
                throw new InvalidOperationException("No console commands have been provided");

            foreach (var command in commands)
                AddCommand(command);
        }

        public ConsoleCommand GetCommandFor(string input)
        {
            return _commands.SingleOrDefault(x => x.Keys.Contains(input) && x.CanExecute());
        }

        public ConsoleCommand[] GetAvailableCommands()
        {
            return _commands
                .Where(x => x.CanExecute())
                .OrderBy(x => x.Keys.First())
                .ToArray();
        }

        private void AddCommand(ConsoleCommand command)
        {
            if (command.Keys.Intersect(_keys).Any())
                throw new InvalidOperationException("Cannot add command as there is an existing command with the same key");

            _commands.Add(command);
            _keys.AddRange(command.Keys);
        }
    }
}