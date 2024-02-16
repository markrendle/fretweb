using FretWeb.Fretboards;

namespace FretWeb.Models;

public class FretboardPageViewModel
{
    public List<FretboardViewModel> Fretboards { get; } = new();

    public string? Title { get; set; }
    public required string Tab { get; init; }
    public required string Tuning { get; init; }
    public int? Frets { get; init; }
    public string? Scale { get; init; }
    public string? Root { get; init; }
    public string? Chord { get; set; }
    public string? Scales { get; set; }
    public string? Modes { get; set; }
    public string? Arpeggios { get; set; }
    public string? Chords { get; set; }
    public string? PrintLink { get; set; }
    public bool Print { get; set; }
}