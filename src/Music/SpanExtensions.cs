namespace FretWeb.Music;

internal static class SpanExtensions
{
    public static bool Any<T>(this ReadOnlySpan<T> source, Func<T, bool> predicate)
    {
        foreach (var item in source)
        {
            if (predicate(item)) return true;
        }

        return false;
    }

    public static int IndexOf<T>(this Span<T> source, Func<T, bool> predicate)
    {
        for (int i = 0; i < source.Length; i++)
        {
            if (predicate(source[i])) return i;
        }

        return -1;
    }
}