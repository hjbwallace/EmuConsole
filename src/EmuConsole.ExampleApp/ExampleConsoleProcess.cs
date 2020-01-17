using System;
using System.Collections.Generic;

namespace EmuConsole.ExampleApp
{
    public class ExampleConsoleProcess : ConsoleProcess
    {
        public ExampleConsoleProcess(IConsole console, ConsoleOptions options) : base(console, options)
        {
        }

        protected override IEnumerable<ConsoleCommand> GetCommands()
        {
            yield return new ConsoleCommand("g", "Generate a new GUID", OnGenerateGuid);
        }

        private void OnGenerateGuid()
        {
            _console.WriteLine($"Generated Guid: {Guid.NewGuid()}");
        }
    }
}