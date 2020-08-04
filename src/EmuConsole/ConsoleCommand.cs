using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmuConsole
{
    public class ConsoleCommand
    {
        private readonly Func<Task> _action;
        private readonly Func<bool> _canExecute;

        public ConsoleCommand(string key, string description, Action action, Func<bool> requires = null)
            : this(key, description, ConvertAction(action), requires)
        {
        }

        public ConsoleCommand(string key, string description, Func<Task> action, Func<bool> requires = null)
            : this(new[] { key }, description, action, requires)
        {
        }

        public ConsoleCommand(string key, string description, ConsoleProcess process, Func<bool> requires = null)
            : this(new[] { key }, description, process, requires)
        {
        }

        public ConsoleCommand(IEnumerable<string> keys, string description, ConsoleProcess process, Func<bool> requires = null)
            : this(keys, description, ConvertProcess(process), requires)
        {
        }

        public ConsoleCommand(IEnumerable<string> keys, string description, Action action, Func<bool> requires = null)
            : this(keys, description, ConvertAction(action), requires)
        {
        }

        public ConsoleCommand(IEnumerable<string> keys, string description, Func<Task> action, Func<bool> requires = null)
        {
            if (keys?.Any() != true)
                throw new ArgumentException("Must provide keys for the command");

            if (keys.Any(x => x == null))
                throw new ArgumentException("Null value key provided");

            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Command description must be populated");

            if (action == null)
                throw new ArgumentException("Console action is invalid");

            Keys = keys.OrderBy(x => x.Length).ThenBy(x => x).Distinct().ToArray();
            Description = description;
            _action = action;
            _canExecute = requires ?? (() => true);
        }

        public string Description { get; }

        public string[] Keys { get; }

        public bool CanExecute() => _canExecute();

        public Task Execute() => _action();

        private static Func<Task> ConvertAction(Action action)
        {
            if (action == null)
                return null;

            return () =>
            {
                action?.Invoke();
                return Task.CompletedTask;
            };
        }

        private static Func<Task> ConvertProcess(ConsoleProcess process)
        {
            return process == null 
                ? (Func<Task>)null 
                : (() => process.RunAsync());
        }
    }
}