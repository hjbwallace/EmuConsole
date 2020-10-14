namespace EmuConsole
{
    public static class InputCollectionExtensions
    {
        public static TValue GetSelection<TKey, TValue>(this InputCollection<TKey, TValue> collection, IConsole console, bool writeInline = false)
        {
            return collection.GetSelection(console,
                writeInline ? CollectionWriteStyle.Inline : CollectionWriteStyle.Rows);
        }

        public static TValue[] GetSelection<TKey, TValue>(this MultipleInputCollection<TKey, TValue> collection, IConsole console, bool writeInline = false)
        {
            return collection.GetSelection(console,
                writeInline ? CollectionWriteStyle.Inline : CollectionWriteStyle.Rows);
        }
    }
}