using System;
using System.Collections.Generic;

namespace EmuConsole
{
    public static class PromptInputSelectionExtensions
    {
        public static T PromptInputSelection<T>(this IConsole console,
                                                IDictionary<object, T> source,
                                                Func<object, T, object> descriptionSelector = null,
                                                bool writeInline = false)
        {
            var indexCollection = new InputCollection<T>(source, descriptionSelector);
            return indexCollection.GetSelection(console, writeInline);
        }

        public static T[] PromptInputSelections<T>(this IConsole console,
                                                   IDictionary<object, T> source,
                                                   Func<object, T, object> descriptionSelector = null,
                                                   bool allowEmpty = false,
                                                   bool writeInline = false)
        {
            var indexCollection = new MultipleInputCollection<T>(source, descriptionSelector, allowEmpty);
            return indexCollection.GetSelection(console, writeInline);
        }
    }
}