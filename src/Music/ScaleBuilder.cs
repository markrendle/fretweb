using FretWeb.Music.NoteTypes;

namespace FretWeb.Music;

public class ScaleBuilder
{
    private readonly string _name;
    private readonly int[] _intervals;

    private ScaleBuilder(string name, int[] intervals)
    {
        _name = name;
        _intervals = intervals;
    }

    public static ScaleBuilder Create(string name, params int[] intervals) => new(name, intervals);

    public ScaleSet WithNotes(params Note[] notes)
    {
        var scales = new List<Scale>();

        foreach (var note in notes)
        {
            scales.Add(BuildScale(note));
        }

        return new ScaleSet(_name, scales.ToArray());
    }

    private Scale BuildScale(Note root)
    {
        var note = root;
        var notes = new List<Note>
        {
            note
        };

        var sign = root.Sign;

        foreach (var interval in _intervals)
        {
            note = note.AddSemitones(interval);

            switch (sign)
            {
                case Sign.Natural:
                    sign = note.Sign;
                    break;
                case Sign.Flat:
                    if (note.IsSharp) note = note.Alt;
                    break;
                case Sign.Sharp:
                    if (note.IsFlat) note = note.Alt;
                    break;
            }
            
            notes.Add(note);
        }

        for (int i = 0; i < notes.Count; i++)
        {
            if (!FixDuplicates(notes))
            {
                break;
            }
        }

        return new Scale(notes.ToArray());
    }

    private void Normalize(List<Note> notes)
    {
        
    }

    private bool FixDuplicates(List<Note> scale)
    {
        for (int i = 0; i < scale.Count - 1; i++)
        {
            var note = scale[i];
            var next = scale[i + 1];

            if (note.Letter == next.Letter)
            {
                if (note.IsSharp || note.IsFlat)
                {
                    scale[i] = note.Alt;
                    return true;
                }
            }
        }

        return false;
    }
}