namespace Codex.Utilities
{
    public static partial class CollectionUtilities
    {
        public static void Add<TKey, TValue>(this IDictionary<TKey, TValue> map, IEnumerable<KeyValuePair<TKey, TValue>> entries)
        {
            foreach (var entry in entries)
            {
                map.Add(entry.Key, entry.Value);
            }
        }

        public static TResult[] SelectArray<T, TResult>(this IReadOnlyCollection<T> items, Func<T, TResult> selector)
        {
            TResult[] results = new TResult[items.Count];
            int i = 0;
            foreach (var item in items)
            {
                results[i] = selector(item);
                i++;
            }

            return results;
        }

        public static IReadOnlyList<TResult> SelectList<T, TResult>(this IReadOnlyCollection<T> items, Func<T, TResult> selector)
        {
            return SelectArray(items, selector);
        }

        public static T SingleOrDefaultNoThrow<T>(this IEnumerable<T> items)
        {
            int count = 0;
            T result = default(T);
            foreach (var item in items)
            {
                if (count == 0)
                {
                    result = item;
                }
                else
                {
                    return default(T);
                }

                count++;
            }

            return result;
        }

        public static List<T> AsList<T>(this IEnumerable<T> items)
        {
            if (items is List<T>)
            {
                return (List<T>)items;
            }
            else
            {
                return items.ToList();
            }
        }

        public static IReadOnlyList<T> AsReadOnlyList<T>(this IEnumerable<T> items)
        {
            if (items is IReadOnlyList<T>)
            {
                return (IReadOnlyList<T>)items;
            }
            else
            {
                return items.ToList();
            }
        }

        public static IEnumerable<T> SortedUnique<T>(this IEnumerable<T> items, IComparer<T> comparer)
        {
            T lastItem = default(T);
            bool hasLastItem = false;
            foreach (var item in items)
            {
                if (!hasLastItem || comparer.Compare(lastItem, item) != 0)
                {
                    hasLastItem = true;
                    lastItem = item;
                    yield return item;
                }
            }
        }

        public static IEnumerable<T> ExclusiveInterleave<T>(this IEnumerable<T> items1, IEnumerable<T> items2, IComparer<T> comparer)
        {
            var enumerator1 = items1.GetEnumerator();
            var enumerator2 = items2.GetEnumerator();

            bool hasCurrent1 = TryMoveNext(enumerator1, out var current1);
            bool hasCurrent2 = TryMoveNext(enumerator2, out var current2);

            while (hasCurrent1 || hasCurrent2)
            {
                while (hasCurrent1)
                {
                    if (!hasCurrent2 || comparer.Compare(current1, current2) <= 0)
                    {
                        yield return current1;

                        // Skip over matching spans from second list
                        while (hasCurrent2 && comparer.Compare(current1, current2) == 0)
                        {
                            hasCurrent2 = TryMoveNext(enumerator2, out current2);
                        }
                    }
                    else
                    {
                        break;
                    }

                    hasCurrent1 = TryMoveNext(enumerator1, out current1);
                }

                while (hasCurrent2)
                {
                    if (!hasCurrent1 || comparer.Compare(current1, current2) > 0)
                    {
                        yield return current2;
                    }
                    else
                    {
                        break;
                    }

                    hasCurrent2 = TryMoveNext(enumerator2, out current2);
                }
            }
        }

        public static bool TryMoveNext<T>(this IEnumerator<T> enumerator1, out T current)
        {
            if (enumerator1.MoveNext())
            {
                current = enumerator1.Current;
                return true;
            }
            else
            {
                current = default(T);
                return false;
            }
        }

        public static TValue GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue = default(TValue))
        {
            TValue value;
            if (!dictionary.TryGetValue(key, out value))
            {
                value = defaultValue;
            }

            return value;
        }

        public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue = default(TValue))
        {
            TValue value;
            if (!dictionary.TryGetValue(key, out value))
            {
                dictionary[key] = defaultValue;
                value = defaultValue;
            }

            return value;
        }

        public static Dictionary<TKey, TValue> ToDictionarySafe<TValue, TKey>(this IEnumerable<TValue> source, Func<TValue, TKey> keySelector, IEqualityComparer<TKey> comparer = null, bool overwrite = false)
        {
            Dictionary<TKey, TValue> result = new Dictionary<TKey, TValue>(comparer ?? EqualityComparer<TKey>.Default);

            foreach (var element in source)
            {
                var key = keySelector(element);

                if (overwrite)
                {
                    result[key] = element;
                }
                else if (!result.ContainsKey(key))
                {
                    result.Add(key, element);
                }
            }

            return result;
        }

        /// <summary>
        /// Converts sequence to dictionary, but accepts duplicate keys. First will win.
        /// </summary>
        public static Dictionary<TKey, TValue> ToDictionarySafe<T, TKey, TValue>(this IEnumerable<T> source, Func<T, TKey> keySelector,
            Func<T, TValue> valueSelector, IEqualityComparer<TKey> comparer = null, bool overwrite = false)
        {
            Dictionary<TKey, TValue> result = new Dictionary<TKey, TValue>(comparer ?? EqualityComparer<TKey>.Default);

            foreach (var element in source)
            {
                var key = keySelector(element);
                var value = valueSelector(element);

                if (overwrite)
                {
                    result[key] = value;
                }
                else if (!result.ContainsKey(key))
                {
                    result.Add(key, value);
                }
            }

            return result;
        }
    }
}
