using System;
using System.Collections.Generic;
using System.Linq;

namespace EmuConsole
{
    public class MultipleIndexCollection<TEntity> : MultipleInputCollection<int, TEntity>
    {
        public MultipleIndexCollection(IEnumerable<KeyValuePair<int, TEntity>> source,
                                       Func<int, TEntity, object> descriptionSelector = null,
                                       bool allowEmpty = false)
            : base(source, descriptionSelector, allowEmpty)
        {
        }

        public MultipleIndexCollection(IEnumerable<TEntity> source,
                                       Func<int, TEntity, object> descriptionSelector = null,
                                       bool allowEmpty = false) 
            : this(MapSourceFromEnumerable(source), descriptionSelector, allowEmpty)
        {
        }

        public MultipleIndexCollection(IEnumerable<TEntity> source, bool allowEmpty)
            : this(source, x => x?.ToString(), allowEmpty)
        {
        }

        public MultipleIndexCollection(IEnumerable<TEntity> source,
                                       Func<TEntity, object> descriptionSelector,
                                       bool allowEmpty)
            : this(MapSourceFromEnumerable(source), GenerateDescriptionSelector(descriptionSelector), allowEmpty)
        {
        }

        protected override KeyValuePair<int, TEntity>[] MapSource(IEnumerable<KeyValuePair<int, TEntity>> source)
        {
            return source
                .Select((x, i) => new KeyValuePair<int, TEntity>(i, x.Value))
                .ToArray();
        }

        protected override IList<KeyValuePair<string, string>> GenerateDisplay(IEnumerable<KeyValuePair<int, TEntity>> source)
        {
            var length = source.Count();
            var padSize = length.ToString().Length;

            return source.Select(x => new KeyValuePair<string, string>
            (
                GetKey(x.Key).PadLeft(padSize),
                GetDescription(x.Key, x.Value)
            )).ToList();
        }

        private static Func<int, TEntity, object> GenerateDescriptionSelector(Func<TEntity, object> descriptionSelector)
        {
            return descriptionSelector != null
                ? ((key, value) => descriptionSelector(value))
                : (Func<int, TEntity, object>)null;
        }

        private static IDictionary<int, TEntity> MapSourceFromEnumerable(IEnumerable<TEntity> source)
        {
            return source?
                .Select((x, i) => new KeyValuePair<int, TEntity>(i, x))
                .ToDictionary(x => x.Key, x => x.Value);
        }
    }
}