using FretWeb.Fretboards;

namespace FretWeb.Models;

public class FretboardViewModel
{
    public FretboardViewModel(Fretboard fretboard, string id, string title)
    {
        Fretboard = fretboard;
        Id = id;
        Title = title;
    }

    public Fretboard Fretboard { get; }
    public string Id { get; }
    public string Title { get; }
}