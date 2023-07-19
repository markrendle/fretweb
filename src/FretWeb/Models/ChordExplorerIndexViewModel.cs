using FretWeb.Music;

namespace FretWeb.Models;

public class ChordExplorerIndexViewModel
{
    public ChordExplorerIndexViewModel(Arpeggio[] chords)
    {
        Chords = chords;
    }

    public Arpeggio[] Chords { get; }
}