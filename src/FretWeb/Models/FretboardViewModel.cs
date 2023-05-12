using FretWeb.Fretboards;
using FretWeb.Music;

namespace FretWeb.Models;

public class FretboardViewModel
{
    public FretboardViewModel(Fretboard fretboard)
    {
        Fretboard = fretboard;
    }

    public Fretboard Fretboard { get; }

    public string? Title { get; set; }
    public required string Tab { get; init; }
    public required string OpenNotes { get; init; }
    public int? Frets { get; init; }
    public string? Scale { get; init; }
    public string? Root { get; init; }
    public string? Chord { get; set; }
}