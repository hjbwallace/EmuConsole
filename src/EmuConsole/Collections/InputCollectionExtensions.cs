namespace EmuConsole
{
    public static class InputCollectionExtensions
    {
        public static TEntity GetSelection<TEntity>(this InputCollection<TEntity> collection, IConsole console, bool writeInline = false)
        {
            return collection.GetSelection(console,
                writeInline ? CollectionWriteStyle.Inline : CollectionWriteStyle.Rows);
        }

        public static TEntity[] GetSelection<TEntity>(this MultipleInputCollection<TEntity> collection, IConsole console, bool writeInline = false)
        {
            return collection.GetSelection(console,
                writeInline ? CollectionWriteStyle.Inline : CollectionWriteStyle.Rows);
        }
    }
}