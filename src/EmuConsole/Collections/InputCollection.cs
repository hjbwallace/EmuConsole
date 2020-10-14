using System;
using System.Collections.Generic;
using System.Linq;

namespace EmuConsole
{
    public class InputCollection<TEntity> : InputCollectionBase<TEntity>
    {
        private readonly bool _isOptional;
        private readonly string _defaultValue;

        public InputCollection(IDictionary<object, TEntity> source,
                               Func<object, TEntity, object> descriptionSelector = null,
                               bool isOptional = false,
                               string defaultValue = default)
            : base(source, descriptionSelector)
        {
            _isOptional = isOptional;
            _defaultValue = defaultValue;
        }

        public TEntity GetSelection(IConsole console, CollectionWriteStyle style)
        {
            console.WriteLine();
            console.WriteCollection(_display, style);

            var keys = _source.Keys.ToArray();

            var input = _isOptional
                ? console.PromptInput(null, keys, _defaultValue)
                : console.PromptInput(null, keys);

            return input == null ? default : _source[input];
        }
    }
}