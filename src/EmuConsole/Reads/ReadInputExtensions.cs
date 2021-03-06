﻿using EmuConsole.Extensions;
using System.Linq;

namespace EmuConsole
{
    public static class ReadInputExtensions
    {
        public static string ReadFormatted(this IConsole console)
        {
            return console.ReadLine()?.Trim() ?? string.Empty;
        }

        public static string[] ReadDelimitedLine(this IConsole console, char delimiter = ',')
        {
            var input = console.ReadFormatted();
            return input.Split(delimiter)
                .WherePopulated()
                .Select(x => x.Trim())
                .ToArray();
        }
    }
}