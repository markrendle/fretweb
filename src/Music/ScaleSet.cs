using System.Diagnostics.CodeAnalysis;
using FretWeb.Music.NoteTypes;

namespace FretWeb.Music;

public class ScaleSet
{
    private readonly Dictionary<Note, Scale> _scales;

    internal ScaleSet(string name, params Scale[] scales)
    {
        Id = NameToId(name);
        Name = name;
        _scales = scales.ToDictionary(s => s[0], s => s);
    }

    public string Id { get; }
    public string Name { get; }

    public ScaleSet Clone(string name)
    {
        return new ScaleSet(name, Enumerate().ToArray());
    }
    
    public ScaleSet ToDorian() => Shift(1, ScaleNames.Dorian);
    public ScaleSet ToPhrygian() => Shift(2, ScaleNames.Phrygian);
    public ScaleSet ToLydian() => Shift(3, ScaleNames.Lydian);
    public ScaleSet ToMixolydian() => Shift(4, ScaleNames.Mixolydian);
    public ScaleSet ToAeolian() => Shift(5, ScaleNames.Aeolian);
    public ScaleSet ToLocrian() => Shift(6, ScaleNames.Locrian);

    private ScaleSet Shift(int shiftBy, string name)
    {
        var scales = _scales.Values.ToArray();
        for (int i = 0; i < scales.Length; i++)
        {
            scales[i] = scales[i].ShiftLeft(shiftBy);
            if (scales[i][0].IsTheoretical)
            {
                scales[i] = scales[i].Alt();
            }
        }

        return new ScaleSet(name, scales);
    }


    public IEnumerable<Scale> Enumerate() => _scales.Values.OrderBy(s => s[0].Value);

    public Scale Get(Note note) => _scales[note];

    public bool TryGet(Note note, [NotNullWhen(true)] out Scale? scale) =>
        _scales.TryGetValue(note, out scale) || _scales.TryGetValue(note.Alt, out scale);
    
    private static string NameToId(ReadOnlySpan<char> name)
    {
        var flat = DisplayStrings.Flat[0];
        var sharp = DisplayStrings.Sharp[0];
        Span<char> id = stackalloc char[128];
        int index = 0;
        foreach (var c in name)
        {
            if (char.IsAsciiLetter(c) || char.IsAsciiDigit(c))
            {
                id[index] = c;
                index++;
            }
            else if (c == flat)
            {
                "Flat".AsSpan().CopyTo(id.Slice(index));
                index += 4;
            }
            else if (c == sharp)
            {
                "Sharp".AsSpan().CopyTo(id.Slice(index));
                index += 5;
            }
        }

        return new string(id.Slice(0, index));
    }
}