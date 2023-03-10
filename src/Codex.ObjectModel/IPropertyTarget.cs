namespace Codex.ObjectModel
{
    public interface IPropertyTarget
    {
        object CreateClone();
    }

    public interface IPropertyTarget<TSource> : IPropertyTarget
    {
        void CopyFrom(TSource source);
    }

    public static class PropertyTarget
    {
        public static TTarget With<TSource, TTarget>(this TTarget target, TSource source)
            where TTarget : IPropertyTarget<TSource>
        {
            target.CopyFrom(source);
            return target;
        }

        private static TTarget Cast<TTarget, TSource>(TSource source)
        {
            if (source is TTarget typedSource)
            {
                return typedSource;
            }
            else
            {
                return (TTarget)(object)source;
            }
        }

        public static T GetOrCopy<T, TSource>(T target, TSource source)
        {
            if (target is IPropertyTarget)
            {
                return (T)((IPropertyTarget)source).CreateClone();
            }
            else
            {
                return Cast<T, TSource>(source);
            }
        }

        public static List<T> GetOrCopy<T, TSource>(List<T> target, IReadOnlyCollection<TSource> source)
        {
            target.Clear();
            if (source != null)
            {
                target.Capacity = source.Count;
                foreach (var item in source)
                {
                    if (item is IPropertyTarget itemTarget)
                    {
                        target.Add((T)itemTarget.CreateClone());
                    }
                    else
                    {
                        target.Add(Cast<T, TSource>(item));
                    }
                }
            }
            return target;
        }

        //public static TTarget Copy<T, TTarget>(T obj, Func<T, TTarget> copy)
        //    where T : class
        //    where TTarget : class, T
        //{
        //    if (obj == null) return null;
        //    return copy(obj);
        //}
    }
}