using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmuConsole
{
    public abstract class ConsoleProcess
    {
        protected readonly IConsole _console;
        protected readonly ConsoleCommandCollection _commands;
        protected readonly ConsoleOptions _options;
        private bool _isRunning;

        public ConsoleProcess(IConsole console, ConsoleOptions options)
        {
            _console = console ?? throw new ArgumentNullException(nameof(console));
            _options = options ?? throw new ArgumentNullException(nameof(options));

            _console.Initialise(_options);

            var commands = new[] { GetHelpCommand() }
                .Concat(GetCommands())
                .Concat(new[] { GetExitCommand() });

            _commands = new ConsoleCommandCollection(commands);
        }

        public async Task RunAsync()
        {
            _isRunning = true;

            DisplayHeading();
            DisplayAvailableActions();

            while (_isRunning)
            {
                GetInputCommand().Execute();

                _console.WriteLine();
                await Task.CompletedTask;

                if (_options.AlwaysDisplayCommands && _isRunning)
                    DisplayAvailableActions();
            }

            DisplayClosing();
        }

        protected virtual void DisplayHeading()
        {
        }

        protected virtual void DisplayClosing()
        {
        }

        protected virtual ConsoleCommand GetHelpCommand()
        {
            return new ConsoleCommand(new[] { "?", "help" }, "Display available commands", DisplayAvailableActions);
        }

        protected virtual ConsoleCommand GetExitCommand()
        {
            return new ConsoleCommand(new[] { "b", "back" }, "Return to the previous menu", StopRunning);
        }

        protected void StopRunning() => _isRunning = false;

        protected virtual IEnumerable<ConsoleCommand> GetCommands()
        {
            return new ConsoleCommand[0];
        }

        private ConsoleCommand GetInputCommand()
        {
            var input = _console.PromptInput(null);
            var command = _commands.GetCommandFor(input);

            return command ?? GetInputCommand();
        }

        private void DisplayAvailableActions()
        {
            var commandDictionary = _commands.GetAvailableCommands().ToPrompt();
            _console.WriteCollection(commandDictionary, _options.WriteCommandsInline);
        }
    }
}