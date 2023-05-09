using System.Collections.Immutable;
using FretWeb.Music.NoteTypes;

namespace FretWeb.Music;

public class Scale
{
    internal Scale(params Note[] notes)
    {
        Notes = notes.ToImmutableArray();
    }
    
    public IReadOnlyList<Note> Notes { get; }
}