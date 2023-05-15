using FretWeb.Fretboards;
using FretWeb.Music;

namespace FretWeb.Models;

public class FretboardPageViewModel
{
    public FretboardPageViewModel()
    {
    }

    public Fretboard? Fretboard { get; }

    public List<FretboardViewModel> Fretboards { get; } = new();

    public string? Title { get; set; }
    public required string Tab { get; init; }
    public required string OpenNotes { get; init; }
    public int? Frets { get; init; }
    public string? Scale { get; init; }
    public string? Root { get; init; }
    public string? Chord { get; set; }
    public string? Scales { get; set; }
    public string? Arpeggios { get; set; }
    public string? Chords { get; set; }
    public string? PrintLink { get; set; }
}

public class FretboardViewModel
{
    public FretboardViewModel(Fretboard fretboard, string title)
    {
        Fretboard = fretboard;
        Title = title;
    }

    public Fretboard Fretboard { get; }
    public string Title { get; }
}