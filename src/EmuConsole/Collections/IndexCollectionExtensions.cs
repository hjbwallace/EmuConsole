namespace EmuConsole
{
    public static class IndexCollectionExtensions
    {
        public static TEntity GetSelection<TEntity>(this IndexCollection<TEntity> collection, IConsole console, bool writeInline = false)
        {
            return collection.GetSelection(console, 
                writeInline ? CollectionWriteStyle.Inline : CollectionWriteStyle.Rows);
        }

        public static TEntity[] GetSelection<TEntity>(this MultipleIndexCollection<TEntity> collection, IConsole console, bool writeInline = false)
        {
            return collection.GetSelection(console,
                writeInline ? CollectionWriteStyle.Inline : CollectionWriteStyle.Rows);
        }
    }
}