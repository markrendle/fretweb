using System.Diagnostics.CodeAnalysis;
using FretWeb.Music.NoteTypes;

namespace FretWeb.Music;

public class ScaleSet
{
    private readonly Dictionary<Note, Scale> _scales;

    internal ScaleSet(params Scale[] scales)
    {
        _scales = scales.ToDictionary(s => s[0], s => s);
    }
    
    public required string Name { get; init; }

    public ScaleSet Clone(string name)
    {
        return new ScaleSet(Enumerate().ToArray())
        {
            Name = name
        };
    }
    public ScaleSet ToDorian() => Shift(1, "Dorian");
    public ScaleSet ToPhrygian() => Shift(2, "Phrygian");
    public ScaleSet ToLydian() => Shift(3, "Lydian");
    public ScaleSet ToMixolydian() => Shift(4, "Mixolydian");
    public ScaleSet ToAeolian() => Shift(5, "Aeolian");
    public ScaleSet ToLocrian() => Shift(6, "Locrian");

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

        return new ScaleSet(scales)
        {
            Name = name
        };
    }

    private Scale[] RotateDown(Scale[] scales, int by)
    {
        var rotated = new Scale[12];
        int index = by - 1;
        for (int i = 0; i < 12; i++)
        {
            if (++index > 11)
            {
                index = 0;
            }

            rotated[index] = scales[i];
        }

        return rotated;
    }


    public IEnumerable<Scale> Enumerate() => _scales.Values.OrderBy(s => s[0].Value);

    public Scale Get(Note note) => _scales[note];

    public bool TryGet(Note note, [NotNullWhen(true)] out Scale? scale) => _scales.TryGetValue(note, out scale);
}