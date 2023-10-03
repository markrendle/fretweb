namespace FretWeb.Music;

public static class Keys
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