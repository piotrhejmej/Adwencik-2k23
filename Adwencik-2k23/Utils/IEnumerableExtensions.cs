namespace Adwencik_2k23.Utils
{
    internal static class IEnumerableExtensions
    {
        public static int Mul<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector)
        {
            int result = 1;

            foreach (TSource item in source)
            {
                checked
                { result *= selector(item); }
            }

            return result;
        }
        public static long Mul<TSource>(this IEnumerable<TSource> source, Func<TSource, long> selector)
        {
            long result = 1;

            foreach (TSource item in source)
            {
                checked
                { result *= selector(item); }
            }

            return result;
        }

        public static IEnumerable<IEnumerable<TSource>> BatchWhere<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            List<TSource>? bucket = new List<TSource>();

            foreach (var item in source)
            {
                bucket.Add(item);

                if (!predicate(item))
                    continue;

                yield return bucket;

                bucket = new List<TSource>();
            }

            if (bucket.Any())
                yield return bucket;
        }

        public static IEnumerable<IEnumerable<TSource>> Batch<TSource>(this IEnumerable<TSource> source, int size)
        {
            List<TSource>? bucket = new List<TSource>();
            var count = 0;

            foreach (var item in source)
            {
                count++;
                bucket.Add(item);

                if (count != size)
                    continue;

                yield return bucket;

                count = 0;
                bucket = new List<TSource>();
            }

            if (bucket.Any())
                yield return bucket;
        }
    }
}
