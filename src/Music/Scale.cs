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
}