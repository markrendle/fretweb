using FretWeb.Music;
using FretWeb.Music.NoteTypes;

namespace FretWeb.Models;

public class ChordExplorerViewModel
{
    public ChordExplorerViewModel(Arpeggio chord, Note root)
    {
        Chord = chord;
        Root = root;
    }

    public Arpeggio Chord { get; }
    public Note Root { get; }

    public IEnumerable<ScaleSet> GetModes()
    {
        var notes = Chord.GetNotes(Root);
        
        foreach (var mode in Scales.EnumerateModes())
        {
            if (mode.TryGet(Root, out var scale))
            {
                if (notes.All(n => scale.Contains(n)))
                {
                    yield return mode;
                }
            }
        }
    }
}