using System;
using System.Collections.Generic;
using System.Linq;

namespace EmuConsole
{
    public class MultipleInputCollection<TEntity> : InputCollectionBase<TEntity>
    {
        private readonly bool _allowEmpty;

        public MultipleInputCollection(IDictionary<object, TEntity> source,
                                       Func<object, TEntity, object> descriptionSelector = null,
                                       bool allowEmpty = false)
            : base(source, descriptionSelector)
        {
            _allowEmpty = allowEmpty;
        }

        public TEntity[] GetSelection(IConsole console, bool writeInline = false)
        {
            console.WriteLine();
            console.WriteCollection(_display, writeInline);

            var inputs = console.PromptInputs(null, _source.Keys.ToArray(), _allowEmpty);
            return inputs.Select(x => _source[x]).ToArray();
        }
    }
}