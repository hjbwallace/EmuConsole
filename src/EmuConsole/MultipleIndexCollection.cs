using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace EmuConsole
{
    public class MultipleIndexCollection<TEntity>
    {
        private readonly IDictionary<int, TEntity> _source;
        private readonly IList<KeyValuePair<string, string>> _display;
        private readonly bool _allowEmpty;

        public MultipleIndexCollection(IEnumerable<TEntity> source, bool allowEmpty)
            : this(source, x => x?.ToString(), allowEmpty)
        {
        }

        public MultipleIndexCollection(IEnumerable<TEntity> source, Func<TEntity, object> descriptionSelector, bool allowEmpty)
        {
            if (!source.Any())
                throw new ArgumentException("Source must be populated");

            _source = new ConcurrentDictionary<int, TEntity>();
            _display = new List<KeyValuePair<string, string>>();

            var length = source.Count();
            var padSize = length.ToString().Length;

            for (int i = 0; i < length; i++)
            {
                var item = source.ElementAt(i);

                _source.Add(i, item);
                _display.Add(new KeyValuePair<string, string>(i.ToString().PadLeft(padSize), descriptionSelector(item).ToString()));
            }

            _allowEmpty = allowEmpty;
        }

        public TEntity[] GetSelection(IConsole console)
        {
            console.WriteLine();
            console.WriteCollection(_display);

            var inputs = console.PromptInts(null, _source.Keys.ToArray(), _allowEmpty);
            return inputs.Select(x => _source[x]).ToArray();
        }
    }
}