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

    public void SetBadges(Chord chord, Note rootNote)
    {
        foreach (var fretString in _strings)
        {
            if (fretString.Note.IsEquivalentTo(rootNote))
            {
                fretString.Badge = rootNote.Display;
                fretString.IsRoot = true;
                continue;
            }

            fretString.IsRoot = false;
            fretString.Badge = string.Empty;

            foreach (var chordNote in chord.AsSpan().Slice(1))
            {
                var number = chordNote.Number;
                switch (chordNote.Sign)
                {
                    case Sign.Flat:
                        --number;
                        break;
                    case Sign.Sharp:
                        ++number;
                        break;
                    case Sign.FlatFlat:
                        number -= 2;
                        break;
                    case Sign.SharpSharp:
                        number += 2;
                        break;
                }
                var note = rootNote.AddSemitones(number);
                if (fretString.Note.IsEquivalentTo(note))
                {
                    fretString.Badge = $"{chordNote.Sign.GetString()}{chordNote.Number}";
                }
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