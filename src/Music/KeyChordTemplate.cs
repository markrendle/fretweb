using FretWeb.Music.NoteTypes;

namespace FretWeb.Music;

public class KeyChordTemplate
{
    public KeyChordTemplate(int number, KeyChordType chordType, KeyChordMode chordMode)
    {
        Number = ToRoman(number, chordType);
        ChordType = chordType;
        ChordMode = chordMode;
    }

    public string Number { get; }
    public KeyChordType ChordType { get; }
    public KeyChordMode ChordMode { get; }

    private static string ToRoman(int number, KeyChordType chordType)
    {
        var roman = number switch
        {
            1 => "i",
            2 => "ii",
            3 => "iii",
            4 => "iv",
            5 => "v",
            6 => "vi",
            7 => "vii",
        };
        if (chordType == KeyChordType.Major) roman = roman.ToUpperInvariant();
        return roman;
    }

    public KeyChord Create(int number, Note root)
    {
        var arpeggio = ChordType switch
        {
            KeyChordType.Major => Arpeggios.Major7th,
            KeyChordType.Minor => Arpeggios.Minor7th,
            KeyChordType.Diminished => Arpeggios.Diminished7th,
            KeyChordType.Dominant => Arpeggios.Dominant7th,
            _ => throw new ArgumentOutOfRangeException()
        };

        var mode = ChordMode switch
        {
            KeyChordMode.Ionian => Scales.Ionian,
            KeyChordMode.Dorian => Scales.Dorian,
            KeyChordMode.Phrygian => Scales.Phrygian,
            KeyChordMode.Lydian => Scales.Lydian,
            KeyChordMode.Mixolydian => Scales.Mixolydian,
            KeyChordMode.Aeolian => Scales.Aeolian,
            KeyChordMode.Locrian => Scales.Locrian,
            _ => throw new ArgumentOutOfRangeException()
        };

        return new KeyChord(ToRoman(number, ChordType), root, ChordType, arpeggio, ChordMode, mode);
    }
}