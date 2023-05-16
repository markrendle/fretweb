using System.Collections;
using System.Collections.Immutable;
using FretWeb.Music.NoteTypes;

namespace FretWeb.Music;

public class Scale : IEnumerable<Note>
{
    private readonly Note[] _notes;
    
    internal Scale(params Note[] notes)
    {
        _notes = notes;
    }

    public Note Root => this[0];

    public Note this[int index]
    {
        get
        {
            if (index < 0 || index >= _notes.Length)
            {
                throw new IndexOutOfRangeException();
            }

            return _notes[index];
        }
    }

    public ReadOnlySpan<Note> AsSpan() => _notes.AsSpan();

    public IEnumerator<Note> GetEnumerator() => _notes.AsEnumerable().GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public Scale ShiftLeft(int by)
    {
        var notes = new Note[_notes.Length];
        int index = 0;
        for (int i = by; i < _notes.Length; i++)
        {
            notes[index++] = _notes[i];
        }

        for (int i = 0; i < by; i++)
        {
            notes[index++] = _notes[i];
        }

        return new Scale(notes);
    }

    public Scale Alt()
    {
        var notes = _notes.Select(n => n.Alt).ToArray();
        return new Scale(notes);
    }

    public bool ContainsSharp() => _notes.Any(n => n.Sign is Sign.Sharp or Sign.SharpSharp);
    public bool ContainsFlat() => _notes.Any(n => n.Sign is Sign.Flat or Sign.FlatFlat);
    public bool ContainsTheoretical() => _notes.Any(n => n.Sign is Sign.SharpSharp or Sign.FlatFlat);

    public bool ContainsMixedSigns()
    {
        bool flat = false;
        bool sharp = false;

        foreach (var note in _notes)
        {
            switch (note.Sign)
            {
                case Sign.Natural:
                    break;
                case Sign.Flat:
                case Sign.FlatFlat:
                    if (sharp) return true;
                    flat = true;
                    break;
                case Sign.Sharp:
                case Sign.SharpSharp:
                    if (flat) return true;
                    sharp = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        return false;
    }
}