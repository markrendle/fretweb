using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace FretWeb.Music;

public static class ScaleParser
{
    public static IEnumerable<(string name, int[] intervals)> Load()
    {
        var resourcePath = $"{typeof(ScaleParser).Namespace}.Data.scales.csv";
        using var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcePath);
        if (resource is null) yield break;
        using var reader = new StreamReader(resource);
        while (!reader.EndOfStream)
        {
            if (reader.ReadLine() is { Length: > 0 } line)
            {
                if (TryParse(line, out var name, out var intervals))
                {
                    yield return (name, intervals);
                }
            }
        }
    }

    private static bool TryParse(string line, [NotNullWhen(true)]out string? name, [NotNullWhen(true)]out int[]? intervals)
    {
        var parts = line.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length < 2) goto fail;

        name = parts[0];
        intervals = new int[parts.Length - 1];

        for (int i = 0; i < parts.Length; i++)
        {
            if (!int.TryParse(parts[i], out int interval)) goto fail;
            intervals[i - 1] = interval;
        }

        return true;
        
        fail:
        name = null;
        intervals = null;
        return false;
    }
}