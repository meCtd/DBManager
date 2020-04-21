using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace Framework.Extensions
{
    public static class Extensions
    {
        public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (var item in items)
            {
                action(item);
            }
        }

        public static IEnumerable<T> OrEmpty<T>(this IEnumerable<T> items)
        {
            return items ?? Enumerable.Empty<T>();
        }

        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key,
            TValue defaultValue = default)
        {
            return dictionary.TryGetValue(key, out var result)
                ? result
                : defaultValue;
        }

        public static TValue GetValueOrDefault<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, TKey key,
            TValue defaultValue = default)
        {
            return dictionary.TryGetValue(key, out var result)
                ? result
                : defaultValue;
        }

        public static T GetParent<T>(this T source, Func<T, T> parentSelector)
        {
            var current = source;
            T parent;

            while (!ReferenceEquals(parent = parentSelector(current), null))
            {
                current = parent;
            }

            return current;
        }

        public static T Get<T>(this DbDataReader reader, string columnName)
        {
            return reader.GetFieldValue<T>(reader.GetOrdinal(columnName));
        }
    }
}
