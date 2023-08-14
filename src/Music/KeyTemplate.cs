using FretWeb.Music.NoteTypes;

namespace FretWeb.Music;

public class Keys
{
    public static KeyTemplate Major { get; } = new KeyTemplate("Major", Scales.Major, new[]
    {
        new KeyChordTemplate(1, KeyChordType.Major, KeyChordMode.Ionian),
        new KeyChordTemplate(2, KeyChordType.Minor, KeyChordMode.Dorian),
        new KeyChordTemplate(3, KeyChordType.Minor, KeyChordMode.Phrygian),
        new KeyChordTemplate(4, KeyChordType.Major, KeyChordMode.Lydian),
        new KeyChordTemplate(5, KeyChordType.Dominant, KeyChordMode.Mixolydian),
        new KeyChordTemplate(6, KeyChordType.Minor, KeyChordMode.Aeolian),
        new KeyChordTemplate(7, KeyChordType.Diminished, KeyChordMode.Locrian),
    });
    
    public static KeyTemplate Minor { get; } = new KeyTemplate("Minor", Scales.Minor, new[]
    {
        new KeyChordTemplate(1, KeyChordType.Minor, KeyChordMode.Aeolian),
        new KeyChordTemplate(2, KeyChordType.Diminished, KeyChordMode.Locrian),
        new KeyChordTemplate(3, KeyChordType.Major, KeyChordMode.Ionian),
        new KeyChordTemplate(4, KeyChordType.Minor, KeyChordMode.Dorian),
        new KeyChordTemplate(5, KeyChordType.Minor, KeyChordMode.Phrygian),
        new KeyChordTemplate(6, KeyChordType.Major, KeyChordMode.Lydian),
        new KeyChordTemplate(7, KeyChordType.Dominant, KeyChordMode.Mixolydian),
    });
}

public class Key
{
    public Key(string name, Note[] notes, KeyChord[] chords)
    {
        Name = name;
        Notes = notes;
        Chords = chords;
    }

    public string Name { get; }
    public Note[] Notes { get; }
    public KeyChord[] Chords { get; }
}

public class KeyTemplate
{
    private readonly Dictionary<Note, Key> _cache = new();
    private readonly ScaleSet _scaleSet;
    private readonly KeyChordTemplate[] _chordTemplates;

    public KeyTemplate(string name, ScaleSet scaleSet, KeyChordTemplate[] chordTemplates)
    {
        Name = name;
        _chordTemplates = chordTemplates;
        _scaleSet = scaleSet;
    }

    public string Name { get; }

    public Key Get(Note root)
    {
        if (_cache.TryGetValue(root, out var key)) return key;
        
        var name = $"{root.Display} {Name}";
        var scale = _scaleSet.Get(root);
        var chords = new KeyChord[_chordTemplates.Length];

        for (int i = 0; i < _chordTemplates.Length; i++)
        {
            var note = scale[i];
            chords[i] = _chordTemplates[i].Create(i + 1, note);
        }

        _cache[root] = key = new Key(name, scale.AsSpan().ToArray(), chords);
        return key;
    }
}

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
        KeyChordType.Major => "Major 7th",
        KeyChordType.Minor => "Minor 7th",
        KeyChordType.Diminished => "Diminished 7th",
        KeyChordType.Dominant => "Dominant 7th",
        _ => throw new ArgumentOutOfRangeException()
    };
    
    public KeyChordMode ChordMode { get; }
    public Note[] Arpeggio { get; }
    public Scale Mode { get; }
}

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

public enum KeyType
{
    Major,
    Minor,
}

public enum KeyChordType
{
    Major,
    Minor,
    Diminished,
    Dominant,
}

public enum KeyChordMode
{
    Ionian,
    Dorian,
    Phrygian,
    Lydian,
    Mixolydian,
    Aeolian,
    Locrian,
}
