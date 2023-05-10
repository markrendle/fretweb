using FretWeb.Music.NoteTypes;

namespace FretWeb.Music;

public class ScaleSet
{
    private readonly Dictionary<Note, Scale> _scales;

    internal ScaleSet(params Scale[] scales)
    {
        _scales = scales.ToDictionary(s => s[0], s => s);
    }

    public ScaleSet ToDorian() => Shift(1);
    public ScaleSet ToPhrygian() => Shift(2);
    public ScaleSet ToLydian() => Shift(3);
    public ScaleSet ToMixolydian() => Shift(4);
    public ScaleSet ToAeolian() => Shift(5);
    public ScaleSet ToLocrian() => Shift(6);

    private ScaleSet Shift(int shiftBy)
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

        return new ScaleSet(scales);
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
}