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

public static class Scales
{
    static Scales()
    {
        Ionian = new(
            new Scale(Notes.C, Notes.D, Notes.E, Notes.F, Notes.G, Notes.A, Notes.B),
            new Scale(Notes.DFlat, Notes.EFlat, Notes.F, Notes.GFlat, Notes.AFlat, Notes.BFlat, Notes.C),
            new Scale(Notes.D, Notes.E, Notes.FSharp, Notes.G, Notes.A, Notes.B, Notes.CSharp),
            new Scale(Notes.EFlat, Notes.F, Notes.G, Notes.AFlat, Notes.BFlat, Notes.C, Notes.D),
            new Scale(Notes.E, Notes.FSharp, Notes.GSharp, Notes.A, Notes.B, Notes.CSharp, Notes.DSharp),
            new Scale(Notes.F, Notes.G, Notes.A, Notes.BFlat, Notes.C, Notes.D, Notes.E),
            new Scale(Notes.GFlat, Notes.AFlat, Notes.BFlat, Notes.CFlat, Notes.DFlat, Notes.EFlat, Notes.F),
            new Scale(Notes.G, Notes.A, Notes.B, Notes.C, Notes.D, Notes.E, Notes.FSharp),
            new Scale(Notes.AFlat, Notes.BFlat, Notes.C, Notes.DFlat, Notes.EFlat, Notes.F, Notes.G),
            new Scale(Notes.A, Notes.B, Notes.CSharp, Notes.D, Notes.E, Notes.FSharp, Notes.GSharp),
            new Scale(Notes.BFlat, Notes.C, Notes.D, Notes.EFlat, Notes.F, Notes.G, Notes.A),
            new Scale(Notes.B, Notes.CSharp, Notes.DSharp, Notes.E, Notes.FSharp, Notes.GSharp, Notes.ASharp)
        )
        {
            Name = "Ionian"
        };
        Dorian = Ionian.ToDorian();
        Phrygian = Ionian.ToPhrygian();
        Lydian = Ionian.ToLydian();
        Mixolydian = Ionian.ToMixolydian();
        Aeolian = Ionian.ToAeolian();
        Locrian = Ionian.ToLocrian();

        Major = Ionian.Clone("Major");
        Minor = Aeolian.Clone("Minor");
    }

    public static ScaleSet Major { get; }
    public static ScaleSet Minor { get; }
    
    public static ScaleSet Ionian { get; }
    public static ScaleSet Dorian { get; }
    public static ScaleSet Phrygian { get; }
    public static ScaleSet Lydian { get; }
    public static ScaleSet Mixolydian { get; }
    public static ScaleSet Aeolian { get; }
    public static ScaleSet Locrian { get; }
    
    public static IEnumerable<ScaleSet> Enumerate()
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

    public static ScaleSet? FindByName(string name) => Enumerate().FirstOrDefault(s => s.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
}