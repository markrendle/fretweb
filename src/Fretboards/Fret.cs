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

            fretString.Index = index;

            if (index == 0)
            {
                fretString.Badge = notes[index].Display;
                fretString.IsRoot = true;
                continue;
            }

            int display = index + 1;
            string badge = display.ToString();

            // If we're on e.g. Eflat as the third note of C Minor, then the third note in C Major is E
            // So if the same index note is C Major is equivalent to our current note + a semi-tone,
            // then we're a flat

            if (majorScale is not null)
            {
                var major = majorScale.AsSpan();
                index %= major.Length;
                if (major[index].IsSemitoneHigherThan(note))
                {
                    badge = $"{DisplayStrings.Flat}{display}";
                }
            }

            fretString.Badge = badge;
            fretString.IsRoot = false;
        }
    }

    public void SetBadges(Note[] notes)
    {
        var scale = Scales.Major.Get(notes[0]);
        
        foreach (var fretString in _strings)
        {
            fretString.Badge = "";
            
            if (fretString.Note.IsEquivalentTo(notes[0]))
            {
                fretString.Badge = notes[0].Display;
                fretString.IsRoot = true;
                fretString.Index = 0;
                continue;
            }
            
            fretString.IsRoot = false;

            for (int i = 1; i < notes.Length; i++)
            {
                var note = notes[i];
                if (fretString.Note != note) continue;
                int index = scale.AsSpan().IndexOf(note) + 1;
                fretString.Badge = index.ToString();
                break;
            }
        }
    }

    public void SetBadges(Arpeggio arpeggio, Note[] notes)
    {
        var arpeggioNotes = arpeggio.AsSpan();
        var rootNote = notes[0];
        foreach (var fretString in _strings)
        {
            if (fretString.Note.IsEquivalentTo(rootNote))
            {
                fretString.Badge = rootNote.Display;
                fretString.IsRoot = true;
                fretString.Index = 0;
                continue;
            }

            fretString.IsRoot = false;
            fretString.Badge = string.Empty;

            for (int i = 1; i < notes.Length; i++)
            {
                if (!fretString.Note.IsEquivalentTo(notes[i])) continue;
                var arpeggioNote = arpeggioNotes[i];
                fretString.Badge = arpeggioNote.Sign == Sign.Natural ? arpeggioNote.Number.ToString() : $"{arpeggioNote.Sign.GetString()}{arpeggioNote.Number}";
                fretString.Index = i;
            }
        }
    }

    public void SetBadges(Note note)
    {
        foreach (var fretString in _strings)
        {
            if (fretString.Note.IsEquivalentTo(note))
            {
                fretString.Badge = note.Display;
                fretString.IsRoot = true;
            }
            else
            {
                fretString.Badge = string.Empty;
                fretString.IsRoot = false;
            }
        }
    }
}