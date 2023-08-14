using FretWeb.Music.NoteTypes;

namespace FretWeb.Models;

internal static class KeyRoots
{
    public static readonly Note[] Major =
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
    
    public static readonly Note[] Minor =
    {
        Music.Notes.C,
        Music.Notes.CSharp,
        Music.Notes.D,
        Music.Notes.EFlat,
        Music.Notes.E,
        Music.Notes.F,
        Music.Notes.FSharp,
        Music.Notes.G,
        Music.Notes.GSharp,
        Music.Notes.A,
        Music.Notes.BFlat,
        Music.Notes.B,
    };
}