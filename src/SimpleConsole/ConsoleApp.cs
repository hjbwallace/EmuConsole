using System.Threading.Tasks;

namespace SimpleConsole
{
    public class ConsoleApp
    {
        private readonly IConsole _console;
        private bool _isRunning;

        public ConsoleApp(IConsole console)
        {
            _console = console;
        }

        public async Task RunAsync()
        {
            _isRunning = true;

            _console.WriteLine("Welcome to a console app!");

            while (_isRunning)
            {
                _console.WriteLine("Waiting for imput (or empty to exit): ");
                _console.Write("> ");

                var input = _console.ReadLine();

                if (input == string.Empty)
                    _isRunning = false;

                await Task.CompletedTask;
            }

            _console.WriteLine("Closing!");
        }
    }
}