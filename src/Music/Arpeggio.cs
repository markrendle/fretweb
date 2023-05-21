using FretWeb.Music.NoteTypes;

namespace FretWeb.Music;

public class Arpeggio
{
    private readonly ArpeggioNote[] _notes;

    public Arpeggio(string group, string name, params ArpeggioNote[] notes)
    {
        Group = group;
        Name = name;
        Id = NameToId(name);
        _notes = notes;
    }

    public string Id { get; set; }
    public string Group { get; }
    public string Name { get; }

    public int Count => _notes.Length;
    public ArpeggioNote this[int index] => _notes[index];
    public ReadOnlySpan<ArpeggioNote> AsSpan() => _notes.AsSpan();

    public Note[] GetNotes(Note root)
    {
        var notes = new Note[Count];
        var index = 0;
        notes[index++] = root;

        var scale = Scales.Major.Get(root).AsSpan();

        foreach (var arpeggioNote in _notes.Skip(1))
        {
            var notePosition = (arpeggioNote.Number - 1) % scale.Length;
            var note = scale[notePosition];
            switch (arpeggioNote.Sign)
            {
                case Sign.Flat:
                    note = note.Sign == Sign.Sharp ? note.SubtractSemitone() : note.SubtractSemitone().AsFlat();
                    break;
                case Sign.FlatFlat:
                    note = note.SubtractTone().AsFlat();
                    break;
                case Sign.Sharp:
                    note = note.Sign == Sign.Flat ? note.AddSemitone() : note.AddSemitone().AsSharp();
                    break;
                case Sign.SharpSharp:
                    note = note.AddTone().AsSharp();
                    break;
            }

            notes[index++] = note;
        }

        if (notes.Any(n => n.Sign is Sign.FlatFlat or Sign.SharpSharp))
        {
            notes = Normalizer.TryRemoveDoubleSigns(notes);
        }
        
        if (Normalizer.ContainsRepeatedLetter(notes))
        {
            var flat = Normalizer.ForceFlat(notes);
            if (!(Normalizer.ContainsRepeatedLetter(flat) || flat.Any(n => n.IsTheoretical)))
            {
                return flat;
            }
            var sharp = Normalizer.ForceSharp(notes);
            if (!(Normalizer.ContainsRepeatedLetter(sharp) || sharp.Any(n => n.IsTheoretical)))
            {
                return sharp;
            }
        }

        return notes;
    }

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