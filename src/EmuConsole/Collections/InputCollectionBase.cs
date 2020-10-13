using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace EmuConsole
{
    public abstract class InputCollectionBase<TEntity>
    {
        protected readonly IDictionary<string, TEntity> _source;
        protected readonly IList<KeyValuePair<string, string>> _display;

        public InputCollectionBase(IDictionary<object, TEntity> source, Func<object, TEntity, object> descriptionSelector)
        {
            if (!source.Any())
                throw new ArgumentException("Source must be populated");

            _source = new ConcurrentDictionary<string, TEntity>();
            _display = new List<KeyValuePair<string, string>>();

            var length = source.Count();

            for (int i = 0; i < length; i++)
            {
                var item = source.ElementAt(i);
                var sourceKey = item.Key?.ToString();

                _source.Add(sourceKey, item.Value);
                _display.Add(new KeyValuePair<string, string>(sourceKey, descriptionSelector?.Invoke(item.Key, item.Value)?.ToString() ?? item.Value?.ToString()));
            }
        }
    }
}