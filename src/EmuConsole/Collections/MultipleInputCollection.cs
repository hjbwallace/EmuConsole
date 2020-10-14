using System;
using System.Collections.Generic;
using System.Linq;

namespace EmuConsole
{
    public class MultipleInputCollection<TKey, TEntity> : InputCollectionBase<TKey, TEntity>
    {
        private readonly bool _allowEmpty;

        public MultipleInputCollection(IDictionary<TKey, TEntity> source,
                                       Func<TKey, TEntity, object> descriptionSelector = null,
                                       bool allowEmpty = false)
            : base(source, descriptionSelector)
        {
            _allowEmpty = allowEmpty;
        }

        public TEntity[] GetSelection(IConsole console, CollectionWriteStyle style)
        {
            console.WriteLine();
            console.WriteCollection(_display, style);

            var inputs = console.PromptInputs(null, _source.Keys.ToArray(), _allowEmpty);
            return inputs.Select(x => _source[x]).ToArray();
        }
    }
}