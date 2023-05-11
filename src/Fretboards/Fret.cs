using FretWeb.Music;
using FretWeb.Music.NoteTypes;

namespace FretWeb.Fretboards;

public class Fret
{
    private readonly FretString[] _strings;

    public Fret(int number, FretString[] strings)
    {
        Number = number;
        _strings = strings;
    }
    
    public int Number { get; }
    
    public IReadOnlyList<FretString> Strings => _strings;

    public void ClearBadges()
    {
        foreach (var fretString in _strings)
        {
            fretString.Badge = fretString.Note.Display;
        }
    }

    public void SetBadges(Scale scale, Sign sign)
    {
        Span<Note> notes = new Note[scale.AsSpan().Length];
        scale.AsSpan().CopyTo(notes);
        foreach (ref var note in notes)
        {
            if (!note.IsNatural && note.Sign != sign)
            {
                note = note.Alt;
            }
        }
        
        foreach (var fretString in _strings)
        {
            int index = notes.IndexOf(fretString.Note);
            if (index < 0 && !fretString.Note.IsNatural)
            {
                index = notes.IndexOf(fretString.Note.Alt);
            }
            if (index < 0)
            {
                fretString.Badge = string.Empty;
                fretString.IsRoot = false;
            }
            else if (index == 0)
            {
                fretString.Badge = notes[index].Display;
                fretString.IsRoot = true;
            }
            else
            {
                ++index;
                fretString.Badge = index.ToString();
                fretString.IsRoot = false;
            }
        }
    }
}