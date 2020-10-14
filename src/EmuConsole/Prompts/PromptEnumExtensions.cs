using System;
using System.Linq;

namespace EmuConsole
{
    public static class PromptEnumExtensions
    {
        public static TEnum PromptIndexSelection<TEnum>(this IConsole console, CollectionWriteStyle style = CollectionWriteStyle.Rows) where TEnum : Enum
        {
            var source = Enum.GetValues(typeof(TEnum)).Cast<TEnum>();
            return console.PromptIndexSelection(source, style);
        }

        public static TEnum[] PromptIndexSelections<TEnum>(this IConsole console, bool allowEmpty = false, CollectionWriteStyle style = CollectionWriteStyle.Rows) where TEnum : Enum
        {
            var source = Enum.GetValues(typeof(TEnum)).Cast<TEnum>();
            return console.PromptIndexSelections(source, allowEmpty, style);
        }
    }
}