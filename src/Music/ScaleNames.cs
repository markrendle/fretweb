namespace FretWeb.Music;

public static class ScaleNames
{
    public const string Major = "Major";
    public const string Minor = "Minor";
    public const string Ionian = "Ionian";
    public const string Dorian = "Dorian";
    public const string Phrygian = "Phrygian";
    public const string Lydian = "Lydian";
    public const string Mixolydian = "Mixolydian";
    public const string Aeolian = "Aeolian";
    public const string Locrian = "Locrian";

    public static IEnumerable<string> Enumerate()
    {
        yield return Major;
        yield return Minor;
        yield return Ionian;
        yield return Dorian;
        yield return Phrygian;
        yield return Lydian;
        yield return Mixolydian;
        yield return Aeolian;
        yield return Locrian;
    }
}