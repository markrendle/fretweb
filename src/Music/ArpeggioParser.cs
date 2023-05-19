using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace FretWeb.Music;

public static class ArpeggioParser
{
    public static IEnumerable<Arpeggio> Load()
    {
        var resourcePath = $"{typeof(ArpeggioParser).Namespace}.Data.arpeggios.csv";
        using var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcePath);
        if (resource is null) yield break;
        using var reader = new StreamReader(resource);
        while (!reader.EndOfStream)
        {
            if (reader.ReadLine() is { Length: > 0 } line)
            {
                if (TryParse(line, out var arpeggio))
                {
                    yield return arpeggio;
                }
            }
        }
    }
    
    public static bool TryParse(string source, [NotNullWhen(true)] out Arpeggio? arpeggio)
    {
        var parts = source.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length < 4) goto fail;

        var group = parts[0].Trim('"');
        var name = parts[1].Trim('"').Replace("flat", DisplayStrings.Flat).Replace("sharp", DisplayStrings.Sharp);

        ArpeggioNote[] notes = new ArpeggioNote[parts.Length - 2];

        for (int i = 2; i < parts.Length; i++)
        {
            if (!TryParseNote(parts[i], out var note)) goto fail;
            notes[i - 2] = note;
        }

        arpeggio = new Arpeggio(group, name, notes);
        return true;

        fail:
        arpeggio = default;
        return false;
    }

    private static bool TryParseNote(string str, out ArpeggioNote note)
    {
        if (str.StartsWith("flatflat", StringComparison.OrdinalIgnoreCase))
        {
            str = str.Substring(8);
            if (int.TryParse(str, out var value))
            {
                note = FlatFlat(value);
                return true;
            }
        }
        else if (str.StartsWith("flat", StringComparison.OrdinalIgnoreCase))
        {
            str = str.Substring(4);
            if (int.TryParse(str, out var value))
            {
                note = Flat(value);
                return true;
            }
        }
        else if (str.StartsWith("sharpsharp", StringComparison.OrdinalIgnoreCase))
        {
            str = str.Substring(10);
            if (int.TryParse(str, out var value))
            {
                note = SharpSharp(value);
                return true;
            }
        }
        else if (str.StartsWith("sharp", StringComparison.OrdinalIgnoreCase))
        {
            str = str.Substring(5);
            if (int.TryParse(str, out var value))
            {
                note = Sharp(value);
                return true;
            }
        }
        else
        {
            if (int.TryParse(str, out var value))
            {
                note = value;
                return true;
            }
        }

        note = default;
        return false;
    }

    private static ArpeggioNote Flat(int number) => new(number, Sign.Flat);
    private static ArpeggioNote FlatFlat(int number) => new(number, Sign.FlatFlat);
    private static ArpeggioNote Sharp(int number) => new(number, Sign.Sharp);
    private static ArpeggioNote SharpSharp(int number) => new(number, Sign.SharpSharp);
}