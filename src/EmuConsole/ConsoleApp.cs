using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmuConsole
{
    public class ConsoleApp
    {
        protected readonly IConsole _console;
        protected readonly ConsoleCommandCollection _commands;
        protected readonly ConsoleOptions _options;
        private bool _isRunning;

        public ConsoleApp(IConsole console = null, ConsoleOptions options = null)
        {
            _console = console ?? new StandardConsole();
            _options = options ?? new ConsoleOptions();

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

            _console.WriteWarning("Closing!");
        }

        protected virtual void DisplayHeading()
        {
        }

        protected virtual ConsoleCommand GetHelpCommand()
        {
            return new ConsoleCommand(new[] { "?", "help" }, "Display available commands", DisplayAvailableActions);
        }

        protected virtual ConsoleCommand GetExitCommand()
        {
            return new ConsoleCommand(new[] { "x", "exit" }, "Exit the application", () => _isRunning = false);
        }

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
            _console.WriteCollection(commandDictionary);
        }
    }
}