using EmuConsole.ExampleApp.Processes;
using EmuConsole.ExampleApp.Services;
using System;
using System.Threading.Tasks;

namespace EmuConsole.ExampleApp
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var console = new StandardConsole(args);
            var options = new ConsoleOptions
            {
                Title = "An Example App",
                AlwaysDisplayCommands = true,
                WriteCommandsInline = false,
                InvalidPromptTemplate = "INVALID: {0}",
                InvalidPromptsTemplate = "INVALID",
            };

            var exampleProcess = BuildExampleProcess(console);

            await new ExampleConsoleApp(console, options, exampleProcess).RunAsync();
        }

        private static ExampleProcess BuildExampleProcess(IConsole console)
        {
            var options = new ConsoleOptions
            {
                Title = "An example process within the app",
                AlwaysDisplayCommands = false,
                WriteCommandsInline = false,
                InvalidPromptTemplate = "INVALID: {0}",
                InvalidPromptsTemplate = "INVALID",
                PromptColor = ConsoleColor.Green,
                HighlightColor = ConsoleColor.Yellow,
            };

            return new ExampleProcess(console, options, new GuidGenerator());
        }
    }
}