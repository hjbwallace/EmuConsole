using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmuConsole
{
    public abstract class ConsoleProcess
    {
        protected readonly IConsole _console;
        protected readonly ConsoleOptions _options;
        private ConsoleCommandCollection _commands;
        private bool _isRunning;

        public ConsoleProcess(IConsole console, ConsoleOptions options)
        {
            _console = console ?? throw new ArgumentNullException(nameof(console));
            _options = options ?? throw new ArgumentNullException(nameof(options));

            _console.Initialise(_options);
        }

        public async Task RunAsync()
        {
            _isRunning = true;
            _commands = GetCommandCollection();

            DisplayHeading();
            DisplayAvailableActions();

            while (_isRunning)
            {
                try
                {
                    var command = GetInputCommand();
                    await command.Execute();
                }
                catch (Exception ex)
                {
                    HandleException(ex);
                }

                _console.WriteLine();

                if (_options.AlwaysDisplayCommands && _isRunning)
                    DisplayAvailableActions();
            }

            DisplayClosing();
        }

        protected virtual void HandleException(Exception ex)
        {
            _console.WriteException(ex);
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

        private ConsoleCommandCollection GetCommandCollection()
        {
            var commands = new List<ConsoleCommand>();

            if (!_options.AlwaysDisplayCommands)
                commands.Add(GetHelpCommand());

            commands.AddRange(GetCommands());
            commands.Add(GetExitCommand());

            return new ConsoleCommandCollection(commands);
        }
    }
}