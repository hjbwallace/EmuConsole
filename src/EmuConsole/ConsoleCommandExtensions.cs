using System.Collections.Generic;
using System.Linq;

namespace EmuConsole
{
    public static class ConsoleCommandExtensions
    {
        public static IList<KeyValuePair<string, string>> ToPrompt(this IEnumerable<ConsoleCommand> commands)
        {
            return commands.ToDictionary(
                key => string.Join("|", key.Keys),
                value => value.Description).ToList();
        }
    }
}