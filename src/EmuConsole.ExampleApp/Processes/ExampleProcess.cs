using EmuConsole.ExampleApp.Services;
using System.Collections.Generic;

namespace EmuConsole.ExampleApp.Processes
{
    public class ExampleProcess : ConsoleProcess
    {
        private readonly IGuidGenerator _guidGenerator;

        public ExampleProcess(IConsole console, ConsoleOptions options, IGuidGenerator guidGenerator)
            : base(console, options)
        {
            _guidGenerator = guidGenerator;
        }

        protected override IEnumerable<ConsoleCommand> GetCommands()
        {
            yield return new ConsoleCommand("g", "Generate a new GUID", OnGenerateGuid);
        }

        private void OnGenerateGuid()
        {
            _console.WriteLine($"Generated Guid: {_guidGenerator.Generate()}");
        }
    }
}