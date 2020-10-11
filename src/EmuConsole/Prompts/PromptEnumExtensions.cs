using System;
using System.Linq;

namespace EmuConsole
{
    public static class PromptEnumExtensions
    {
        public static TEnum PromptEnumIndexSelection<TEnum>(this IConsole console, bool writeInline = false) where TEnum : Enum
        {
            var source = Enum.GetValues(typeof(TEnum)).Cast<TEnum>();
            return console.PromptIndexSelection(source, writeInline);
        }

        public static TEnum[] PromptEnumIndexSelections<TEnum>(this IConsole console, bool allowEmpty = false, bool writeInline = false) where TEnum : Enum
        {
            var source = Enum.GetValues(typeof(TEnum)).Cast<TEnum>();
            return console.PromptIndexSelections(source, allowEmpty, writeInline);
        }
    }
}