using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleConsole
{
    public class ConsoleApp
    {
        private readonly IConsole _console;
        private readonly ConsoleCommandCollection _commands;
        private readonly ConsoleOptions _options;
        private bool _isRunning;

        public ConsoleApp(IConsole console, ConsoleOptions options = null)
        {
            _console = console ?? new StandardConsole();
            _options = options ?? new ConsoleOptions();

            _console.Initialise(_options);

            var baseCommands = new[]
            {
                GetHelpCommand(),
                GetExitCommand()
            };

            var allCommands = baseCommands.Concat(GetCommands());

            _commands = new ConsoleCommandCollection(allCommands);
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
            }

            _console.WriteLine("Closing!");
        }

        protected virtual void DisplayHeading()
        {
            _console.WriteLine("Welcome to a console app!");
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
            var availableCommands = _commands.GetAvailableCommands();

            foreach (var command in availableCommands)
            {
                _console.Write($"[{string.Join("|", command.Keys)}] ");
                _console.WriteLine(command.Description);
            }
        }
    }
}