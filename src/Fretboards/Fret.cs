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

        if (!Scales.Major.TryGet(scale.Root, out var majorScale))
        {
            Scales.Major.TryGet(scale.Root.Alt, out majorScale);
        }
        
        foreach (var fretString in _strings)
        {
            var note = fretString.Note;
            
            int index = notes.IndexOf(note);
            if (index < 0 && !fretString.Note.IsNatural)
            {
                note = note.Alt;
                index = notes.IndexOf(note);
            }
            if (index < 0)
            {
                fretString.Badge = string.Empty;
                fretString.IsRoot = false;
                continue;
            }

            if (index == 0)
            {
                fretString.Badge = notes[index].Display;
                fretString.IsRoot = true;
                continue;
            }

            int display = index + 1;
            string badge = display.ToString();

            // If we're on e.g. Eflat as the third note of C Minor, then the third note in C Major is E
            // So get the third note of CMajor,

            if (majorScale is not null)
            {
                var major = majorScale.AsSpan();
                index %= major.Length;
                if (!major[index].IsEquivalentTo(note))
                {
                    if (major[index].IsEquivalentTo(note.AddSemitone()))
                    {
                        badge = $"{DisplayStrings.Flat}{display}";
                    }
                }
            }

            fretString.Badge = badge;
            fretString.IsRoot = false;
        }
    }
}