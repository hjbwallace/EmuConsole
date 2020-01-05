using System;
using System.Threading.Tasks;

namespace SimpleConsole
{
    public class ConsoleApp
    {
        private bool _isRunning;

        public async Task RunAsync()
        {
            _isRunning = true;

            Console.WriteLine("Welcome to a console app!");

            while (_isRunning)
            {
                Console.WriteLine("Waiting for imput (or empty to exit): ");
                var input = Console.ReadLine();

                if (input == string.Empty)
                    _isRunning = false;

                await Task.CompletedTask;
            }

            Console.WriteLine("Closing!");
        }
    }
}