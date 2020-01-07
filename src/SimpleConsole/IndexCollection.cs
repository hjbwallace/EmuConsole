using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace SimpleConsole
{
    public class IndexCollection<TEntity>
    {
        private readonly IDictionary<int, TEntity> _source;
        private readonly IList<KeyValuePair<string, string>> _display;

        public IndexCollection(IEnumerable<TEntity> source)
            : this(source, x => x?.ToString())
        {
        }

        public IndexCollection(IEnumerable<TEntity> source, Func<TEntity, object> descriptionSelector)
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
        }

        public TEntity GetSelection(IConsole console)
        {
            console.WriteLine();
            console.WriteCollection(_display);

            var input = console.PromptInputInt(null, _source.Keys.ToArray());
            return _source[input];
        }
    }
}