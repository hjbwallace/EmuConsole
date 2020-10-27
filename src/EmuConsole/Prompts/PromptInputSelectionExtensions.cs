using System;
using System.Collections.Generic;

namespace EmuConsole
{
    public static class PromptInputSelectionExtensions
    {
        public static TValue PromptInputSelection<TKey, TValue>(
            this IConsole console,
            IDictionary<TKey, TValue> source,
            Func<TKey, TValue, object> descriptionSelector = null,
            bool isOptional = false,
            CollectionWriteStyle style = CollectionWriteStyle.Rows)
        {
            var indexCollection = new InputCollection<TKey, TValue>(source, descriptionSelector, isOptional);
            return indexCollection.GetSelection(console, style);
        }

        public static TValue PromptInputSelection<TKey, TValue>(
            this IConsole console,
            IDictionary<TKey, TValue> source,
            Func<TKey, TValue, object> descriptionSelector,
            string defaultValue,
            CollectionWriteStyle style = CollectionWriteStyle.Rows)
        {
            var indexCollection = new InputCollection<TKey, TValue>(source, descriptionSelector, isOptional: true, defaultValue: defaultValue);
            return indexCollection.GetSelection(console, style);
        }

        public static TValue[] PromptInputSelections<TKey, TValue>(
            this IConsole console,
            IDictionary<TKey, TValue> source,
            Func<TKey, TValue, object> descriptionSelector = null,
            bool allowEmpty = false,
            CollectionWriteStyle style = CollectionWriteStyle.Rows)
        {
            var indexCollection = new MultipleInputCollection<TKey, TValue>(source, descriptionSelector, allowEmpty);
            return indexCollection.GetSelection(console, style);
        }
    }
}