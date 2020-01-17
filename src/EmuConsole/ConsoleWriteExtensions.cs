using System;
using System.Collections.Generic;

namespace EmuConsole
{
    public static class ConsoleWriteExtensions
    {
        public static void WriteLine(this IConsole console) => console.WriteLine("");

        public static T WriteLine<T>(this IConsole console, T value) => console.WriteLine(value, null);

        public static T Write<T>(this IConsole console, T value) => console.Write(value, null);

        public static T Write<T>(this IConsole console, T value, ConsoleColor? colour)
        {
            return console.Write(value, new ConsoleWriteOptions { Foreground = colour });
        }

        public static T WriteLine<T>(this IConsole console, T value, ConsoleColor? colour)
        {
            return console.WriteLine(value, new ConsoleWriteOptions { Foreground = colour });
        }

        public static T WriteLineWarning<T>(this IConsole console, T value)
        {
            return console.WriteLine(value, console.Options.WarningColor);
        }

        public static T WriteLineError<T>(this IConsole console, T value)
        {
            return console.WriteLine(value, console.Options.ErrorColor);
        }

        public static T WriteLineHighlight<T>(this IConsole console, T value)
        {
            return console.WriteLine(value, console.Options.HighlightColor);
        }

        public static T WriteLinePrompt<T>(this IConsole console, T value)
        {
            return console.WriteLine(value, console.Options.PromptColor);
        }

        public static T WriteWarning<T>(this IConsole console, T value)
        {
            return console.Write(value, console.Options.WarningColor);
        }

        public static T WriteError<T>(this IConsole console, T value)
        {
            return console.Write(value, console.Options.ErrorColor);
        }

        public static T WriteHighlight<T>(this IConsole console, T value)
        {
            return console.Write(value, console.Options.HighlightColor);
        }

        public static string WritePrompt(this IConsole console)
        {
            return console.Write("> ", console.Options.PromptColor);
        }

        public static void WriteCollection<TKey, TValue>(this IConsole console, IList<KeyValuePair<TKey, TValue>> collection)
        {
            foreach (var command in collection)
            {
                if (console.Options.HighlightPromptOptions)
                    console.WriteHighlight($"[{command.Key}] ");
                else
                    console.Write($"[{command.Key}] ");

                console.WriteLine(command.Value);
            }
        }
    }
}