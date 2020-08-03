using EmuConsole.ExampleApp.Services;
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
                WriteCommandsInline = true,
                InvalidPromptTemplate = "INVALID: {0}",
                InvalidPromptsTemplate = "INVALID",
            };

            await new ExampleConsoleApp(console, options, new GuidGenerator()).RunAsync();
        }
    }
}