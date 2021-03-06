﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace EmuConsole
{
    public abstract class InputCollectionBase<TKey, TEntity, TResult>
    {
        protected readonly KeyValuePair<TKey, TEntity>[] _source;
        private readonly Func<TKey, TEntity, object> _descriptionSelector;

        public InputCollectionBase(IEnumerable<KeyValuePair<TKey, TEntity>> source,
                                   Func<TKey, TEntity, object> descriptionSelector)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (!source.Any())
                throw new ArgumentException("Source must be populated");

            _source = source.ToArray();
            _descriptionSelector = descriptionSelector ?? ((key, value) => value?.ToString());
        }

        public TResult GetSelection(IConsole console, CollectionWriteStyle style)
        {
            return GetSelectionInternal(console, style, _source, true);
        }

        protected abstract TResult GetSelectionInternal(IConsole console,
                                                        CollectionWriteStyle style,
                                                        IList<KeyValuePair<TKey, TEntity>> source,
                                                        bool writeCollection);

        protected virtual IList<KeyValuePair<string, string>> GenerateDisplay(IEnumerable<KeyValuePair<TKey, TEntity>> source)
        {
            return source.Select(x => new KeyValuePair<string, string>
            (
                GetKey(x.Key),
                GetDescription(x.Key, x.Value)
            )).ToList();
        }

        protected string GetDescription(TKey key, TEntity value) => _descriptionSelector(key, value)?.ToString();

        protected string GetKey(TKey key) => key?.ToString();
    }
}