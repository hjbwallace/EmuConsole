using System;
using System.Collections.Generic;
using System.Linq;

namespace EmuConsole
{
    public class ConsoleCommand
    {
        private readonly Action _action;
        private readonly Func<bool> _canExecute;

        public ConsoleCommand(string key, string description, Action action, Func<bool> requires = null)
            : this(new[] { key }, description, action, requires)
        {
        }

        public ConsoleCommand(string key, string description, ConsoleProcess process, Func<bool> requires = null)
            : this(new[] { key }, description, process, requires)
        {
        }

        public ConsoleCommand(IEnumerable<string> keys, string description, ConsoleProcess process, Func<bool> requires = null)
            : this(keys, description, () => process.RunAsync().Wait(), requires)
        {
        }

        public ConsoleCommand(IEnumerable<string> keys, string description, Action action, Func<bool> requires = null)
        {
            if (keys?.Any() != true)
                throw new ArgumentException("Must provide keys for the command");

            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Command description must be populated");

            Keys = keys.OrderBy(x => x.Length).ThenBy(x => x).ToArray();
            Description = description;
            _action = action;
            _canExecute = requires ?? (() => true);
        }

        public string Description { get; }
        public string[] Keys { get; }

        public bool CanExecute() => _canExecute();

        public void Execute() => _action();
    }
}