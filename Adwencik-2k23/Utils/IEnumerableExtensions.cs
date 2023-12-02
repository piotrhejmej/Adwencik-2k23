namespace Adwencik_2k23.Utils
{
    internal static class IEnumerableExtensions
    {
        public static int Mul<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector)
        {
            int result = 1;

            foreach (TSource item in source)
            {
                checked { result *= selector(item); }
            }

            return result;
        }
    }
}
