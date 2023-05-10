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

    public Scale AsDorian() => ShiftLeft(1);
    public Scale AsPhrygian() => ShiftLeft(2);
    
    public Scale AsLydian() => ShiftLeft(3);
    public Scale AsMixolydian() => ShiftLeft(4);
    public Scale AsAeolian() => ShiftLeft(5);
    public Scale AsLocrian() => ShiftLeft(6);

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
}