namespace FretWeb.Utilities;

public static class UrlHelpers
{
    public static string? RemoveFretboard(ReadOnlySpan<char> path, string query, string fretboardId)
    {
        Span<char> output = stackalloc char[path.Length + query.Length];
        output.Fill(' ');
        var working = output;
        
        var index = path.LastIndexOf('/');
        if (index > 0)
        {
            path.Slice(0, index + 1).CopyTo(working);
            path = path.Slice(index + 1);
            working = working.Slice(index + 1);
        }
        
        index = path.IndexOf(fretboardId, StringComparison.OrdinalIgnoreCase);

        if (index < 0) return null;
        
        if (index == 0) // First fretboard
        {
            path = path.Slice(fretboardId.Length);
            if (path.Length == 0) return null; // Can't remove only fretboard
            
            if (path[0] == '+')
            {
                path = path.Slice(1);
            }

            path.CopyTo(working);
            working = working.Slice(path.Length);
        }
        else // Not first fretboard
        {
            var before = path.Slice(0, index - 1);
            before.CopyTo(working);
            working = working.Slice(before.Length);
            path = path.Slice(before.Length);
            var charBefore = path[0];
            
            if (charBefore == '+')
            {
                path = path.Slice(1);
            }

            path = path.Slice(fretboardId.Length);

            if (path.Length > 0)
            {
                if (path[0] == '+')
                {
                    working[0] = '+';
                    working = working.Slice(1);
                    path = path.Slice(1);
                }
                path.CopyTo(working);
                working = working.Slice(path.Length);
            }
        }
        
        query.CopyTo(working);

        return new string(output.Trim());
    }
}