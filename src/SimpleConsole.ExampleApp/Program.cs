using System.Threading.Tasks;

namespace SimpleConsole.ExampleApp
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var console = new StandardConsole();
            await new ConsoleApp(console).RunAsync();
        }
    }
}