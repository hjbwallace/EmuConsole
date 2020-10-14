using System;
using System.Collections.Generic;
using System.Linq;

namespace EmuConsole
{
    public static class ConsoleWriteCollectionExtensions
    {
        public static void WriteCollection<TKey, TValue>(this IConsole console, IList<KeyValuePair<TKey, TValue>> collection, CollectionWriteStyle style)
        {
            if (collection?.Any() != true)
                throw new ArgumentException("Collection to display must be populated");

            var writableCollection = collection.ToDictionary(key => $"[{key.Key}] ", value => value.Value?.ToString()).ToList();

            switch (style)
            {
                case CollectionWriteStyle.Rows:
                    WriteCollectionInternal(console, writableCollection, (x) => console.WriteLine(x));
                    break;

                case CollectionWriteStyle.Inline:
                    WriteCollectionInternal(console, writableCollection, (x) => console.Write(x + " "));
                    break;

                case CollectionWriteStyle.Columns:
                    WriteCollectionColumns(console, writableCollection);
                    break;

                default:
                    throw new ArgumentException("Cannot write collection with style: " + style);
            }
        }

        public static void WriteCollection<TKey, TValue>(this IConsole console, IDictionary<TKey, TValue> collection, bool writeInline = false)
            => console.WriteCollection(collection?.ToArray(), writeInline);

        public static void WriteCollection<TKey, TValue>(this IConsole console, IDictionary<TKey, TValue> collection, CollectionWriteStyle style)
            => console.WriteCollection(collection?.ToArray(), style);

        public static void WriteCollection<TKey, TValue>(this IConsole console, IList<KeyValuePair<TKey, TValue>> collection, bool writeInline = false)
            => console.WriteCollection(collection, writeInline ? CollectionWriteStyle.Inline : CollectionWriteStyle.Rows);

        private static void WriteCollectionColumns(this IConsole console, IList<KeyValuePair<string, string>> collection)
        {
            var maxLength = collection.Max(x => (x.Key + x.Value + " ").Length);
            var columns = console.Dimensions.Width / maxLength;

            var orderedCollection = columns < 2
                ? collection
                : collection
                    .Select((x, i) => new KeyValuePair<string, string>(x.Key, (x.Value + " ").PadRight(maxLength - x.Key.Length)))
                    .ToList();

            var index = 0;

            void WriteColumnEntry(string value)
            {
                if (index >= columns - 1)
                {
                    console.WriteLine(value.TrimEnd());
                    index = 0;
                }
                else
                {
                    console.Write(value);
                    index++;
                }
            }

            WriteCollectionInternal(console, orderedCollection, WriteColumnEntry);
        }

        private static void WriteCollectionInternal(this IConsole console, IList<KeyValuePair<string, string>> collection, Action<string> writeAction)
        {
            int totalCount = collection.Count();
            for (int i = 0; i < totalCount; i++)
            {
                var command = collection[i];

                if (console.Options.HighlightPromptOptions)
                    console.WriteHighlight(command.Key);
                else
                    console.Write(command.Key);

                if (i == totalCount - 1)
                    console.WriteLine(command.Value?.TrimEnd());
                else
                    writeAction(command.Value);
            }
        }
    }
}