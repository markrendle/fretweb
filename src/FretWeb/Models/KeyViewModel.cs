using FretWeb.Music;
using FretWeb.Music.NoteTypes;

namespace FretWeb.Models;

public class KeyViewModel
{
    public Key Key { get; }

    public KeyViewModel(Key key)
    {
        Key = key;
    }
    
    public Note[] Notes =
    {
        Music.Notes.C,
        Music.Notes.DFlat,
        Music.Notes.D,
        Music.Notes.EFlat,
        Music.Notes.E,
        Music.Notes.F,
        Music.Notes.GFlat,
        Music.Notes.G,
        Music.Notes.AFlat,
        Music.Notes.A,
        Music.Notes.BFlat,
        Music.Notes.B,
    };
}