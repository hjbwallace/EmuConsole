using System;

namespace EmuConsole.ExampleApp.Services
{
    public class GuidGenerator : IGuidGenerator
    {
        public Guid Generate() => Guid.NewGuid();
    }
}