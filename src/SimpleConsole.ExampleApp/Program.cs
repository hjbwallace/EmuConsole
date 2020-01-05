using System.Threading.Tasks;

namespace SimpleConsole.ExampleApp
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            await new ConsoleApp().RunAsync();
        }
    }
}