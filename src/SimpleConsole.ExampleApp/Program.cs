using System.Threading.Tasks;

namespace SimpleConsole.ExampleApp
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var console = new StandardConsole();
            var options = new ConsoleOptions
            {
                Title = "An Example App"
            };

            await new ConsoleApp(console, options).RunAsync();
        }
    }
}