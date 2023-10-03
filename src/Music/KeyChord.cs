using FretWeb.Music.NoteTypes;

namespace FretWeb.Music;

public class KeyChord
{
    public KeyChord(string number, Note root, KeyChordType chordType, Arpeggio arpeggio, KeyChordMode chordMode, ScaleSet mode)
    {
        Number = number;
        Root = root;
        ChordType = chordType;
        Arpeggio = arpeggio.GetNotes(root);
        ChordMode = chordMode;
        Mode = mode.Get(root);
    }

    public string Number { get; }
    public Note Root { get; }
    public KeyChordType ChordType { get; }

    public string ChordName => ChordType switch
    {
        KeyChordType.Major => "Major",
        KeyChordType.Minor => "Minor",
        KeyChordType.Diminished => "Diminished",
        KeyChordType.Dominant => "Major",
        _ => throw new ArgumentOutOfRangeException()
    };
    
    public string ChordName7th => ChordType switch
    {
        KeyChordType.Major => "Major 7th",
        KeyChordType.Minor => "Minor 7th",
        KeyChordType.Diminished => "Minor 7th flat 5",
        KeyChordType.Dominant => "Dominant 7th",
        _ => throw new ArgumentOutOfRangeException()
    };
    
    public KeyChordMode ChordMode { get; }
    public Note[] Arpeggio { get; }
    public Scale Mode { get; }
}