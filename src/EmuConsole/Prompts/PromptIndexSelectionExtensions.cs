using System;
using System.Collections.Generic;

namespace EmuConsole
{
    public static class PromptIndexSelectionExtensions
    {
        public static T PromptIndexSelection<T>(this IConsole console, IEnumerable<T> source)
        {
            var indexCollection = new IndexCollection<T>(source);
            return indexCollection.GetSelection(console);
        }

        public static T PromptIndexSelection<T>(this IConsole console, IEnumerable<T> source, Func<T, object> descriptionSelector)
        {
            var indexCollection = new IndexCollection<T>(source, descriptionSelector);
            return indexCollection.GetSelection(console);
        }

        public static T[] PromptIndexSelections<T>(this IConsole console, IEnumerable<T> source, bool allowEmpty = false)
        {
            var indexCollection = new MultipleIndexCollection<T>(source, allowEmpty);
            return indexCollection.GetSelection(console);
        }

        public static T[] PromptIndexSelections<T>(this IConsole console, IEnumerable<T> source, Func<T, object> descriptionSelector, bool allowEmpty = false)
        {
            var indexCollection = new MultipleIndexCollection<T>(source, descriptionSelector, allowEmpty);
            return indexCollection.GetSelection(console);
        }
    }
}