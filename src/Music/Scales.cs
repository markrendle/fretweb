namespace FretWeb.Music;

public static class Scales
{
    static Scales()
    {
        Ionian = new(ScaleNames.Ionian,
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
        );
        Dorian = Ionian.ToDorian();
        Phrygian = Ionian.ToPhrygian();
        Lydian = Ionian.ToLydian();
        Mixolydian = Ionian.ToMixolydian();
        Aeolian = Ionian.ToAeolian();
        Locrian = Ionian.ToLocrian();

        Major = Ionian.Clone(ScaleNames.Major);
        Minor = Aeolian.Clone(ScaleNames.Minor);

        Bebop = ScaleBuilder.Create("Bebop", 2, 2, 1, 2, 2, 1, 1)
            .WithNotes(Notes.C, Notes.DFlat, Notes.D, Notes.EFlat, Notes.E, Notes.F, Notes.GFlat, Notes.G, Notes.AFlat, Notes.A, Notes.BFlat, Notes.B);
        CountryBlues = ScaleBuilder.Create("Country Blues", 3, 1, 3, 2)
            .WithNotes(Notes.C, Notes.DFlat, Notes.D, Notes.EFlat, Notes.E, Notes.F, Notes.GFlat, Notes.G, Notes.AFlat, Notes.A, Notes.BFlat, Notes.B);
        MajorBlues = ScaleBuilder.Create("Major Blues", 2, 1, 1, 3, 1)
            .WithNotes(Notes.C, Notes.DFlat, Notes.D, Notes.EFlat, Notes.E, Notes.F, Notes.GFlat, Notes.G, Notes.AFlat, Notes.A, Notes.BFlat, Notes.B);
        MinorBlues = ScaleBuilder.Create("Minor Blues", 3, 2, 1, 1, 3)
            .WithNotes(Notes.C, Notes.DFlat, Notes.D, Notes.EFlat, Notes.E, Notes.F, Notes.GFlat, Notes.G, Notes.AFlat, Notes.A, Notes.BFlat, Notes.B);
        WholeTone = ScaleBuilder.Create("Whole Tone", 2, 2, 2, 2, 2)
            .WithNotes(Notes.C, Notes.DFlat, Notes.D, Notes.EFlat, Notes.E, Notes.F, Notes.GFlat, Notes.G, Notes.AFlat, Notes.A, Notes.BFlat, Notes.B);
        Diminished = ScaleBuilder.Create("Diminished", 2, 1, 2, 1, 2, 1, 2)
            .WithNotes(Notes.C, Notes.DFlat, Notes.D, Notes.EFlat, Notes.E, Notes.F, Notes.GFlat, Notes.G, Notes.AFlat, Notes.A, Notes.BFlat, Notes.B);
        Augmented = ScaleBuilder.Create("Augmented", 3, 1, 3, 1, 3)
            .WithNotes(Notes.C, Notes.DFlat, Notes.D, Notes.EFlat, Notes.E, Notes.F, Notes.GFlat, Notes.G, Notes.AFlat, Notes.A, Notes.BFlat, Notes.B);
        MajorPentatonic = ScaleBuilder.Create("Major Pentatonic", 2, 2, 3, 2)
            .WithNotes(Notes.C, Notes.DFlat, Notes.D, Notes.EFlat, Notes.E, Notes.F, Notes.GFlat, Notes.G, Notes.AFlat, Notes.A, Notes.BFlat, Notes.B);
        MinorPentatonic = ScaleBuilder.Create("Minor Pentatonic", 3, 2, 2, 3)
            .WithNotes(Notes.C, Notes.DFlat, Notes.D, Notes.EFlat, Notes.E, Notes.F, Notes.GFlat, Notes.G, Notes.AFlat, Notes.A, Notes.BFlat, Notes.B);
        Harmonic = ScaleBuilder.Create("Harmonic", 2, 1, 2, 2, 1, 3)
            .WithNotes(Notes.C, Notes.DFlat, Notes.D, Notes.EFlat, Notes.E, Notes.F, Notes.GFlat, Notes.G, Notes.AFlat, Notes.A, Notes.BFlat, Notes.B);
        Melodic = ScaleBuilder.Create("Melodic", 2, 1, 2, 2, 2, 2)
            .WithNotes(Notes.C, Notes.DFlat, Notes.D, Notes.EFlat, Notes.E, Notes.F, Notes.GFlat, Notes.G, Notes.AFlat, Notes.A, Notes.BFlat, Notes.B);
    }

    public static ScaleSet Harmonic { get; }

    public static ScaleSet Melodic { get; }

    public static ScaleSet MinorPentatonic { get; }

    public static ScaleSet MajorPentatonic { get; }

    public static ScaleSet Augmented { get; }

    public static ScaleSet Diminished { get; }

    public static ScaleSet WholeTone { get; }

    public static ScaleSet MinorBlues { get; }

    public static ScaleSet MajorBlues { get; }

    public static ScaleSet Bebop { get; }
    public static ScaleSet CountryBlues { get; }

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
        yield return MajorPentatonic;
        yield return MinorPentatonic;
        yield return MajorBlues;
        yield return MinorBlues;
        yield return Bebop;
        yield return CountryBlues;
        yield return Diminished;
        yield return Augmented;
        yield return Harmonic;
        yield return Melodic;
        yield return WholeTone;
        
        yield return Ionian;
        yield return Dorian;
        yield return Phrygian;
        yield return Lydian;
        yield return Mixolydian;
        yield return Aeolian;
        yield return Locrian;
    }
    
    public static IEnumerable<ScaleSet> EnumerateScales()
    {
        yield return Major;
        yield return Minor;
        yield return MajorPentatonic;
        yield return MinorPentatonic;
        yield return MajorBlues;
        yield return MinorBlues;
        yield return Bebop;
        yield return CountryBlues;
        yield return Diminished;
        yield return Augmented;
        yield return Harmonic;
        yield return Melodic;
        yield return WholeTone;
    }
    
    public static IEnumerable<ScaleSet> EnumerateModes()
    {
        yield return Ionian;
        yield return Dorian;
        yield return Phrygian;
        yield return Lydian;
        yield return Mixolydian;
        yield return Aeolian;
        yield return Locrian;
    }

    public static ScaleSet? FindById(string id) => Enumerate().FirstOrDefault(s => s.Id.Equals(id, StringComparison.OrdinalIgnoreCase));
}