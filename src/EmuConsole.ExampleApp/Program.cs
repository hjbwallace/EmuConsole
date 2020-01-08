using System.Threading.Tasks;

namespace EmuConsole.ExampleApp
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var console = new StandardConsole();
            var options = new ConsoleOptions
            {
                Title = "An Example App",
                AlwaysDisplayCommands = true
            };

            await new ExampleConsoleApp(console, options).RunAsync();
        }
    }
}