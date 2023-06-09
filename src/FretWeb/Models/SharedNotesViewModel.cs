using FretWeb.Music.NoteTypes;

namespace FretWeb.Models;

public class SharedNotesViewModel
{
    public SharedNotesViewModel(IEnumerable<Note> notes)
    {
        Notes = notes.ToArray();
    }
    
    public Note[] Notes { get; }
}