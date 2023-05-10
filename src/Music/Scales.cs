namespace FretWeb.Music;

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
        );
        Dorian = Ionian.ToDorian();
        Phrygian = Ionian.ToPhrygian();
        Lydian = Ionian.ToLydian();
        Mixolydian = Ionian.ToMixolydian();
        Aeolian = Ionian.ToAeolian();
        Locrian = Ionian.ToLocrian();
    }

    public static ScaleSet Major => Ionian;
    public static ScaleSet Minor => Aeolian;
    
    public static ScaleSet Ionian { get; }
    public static ScaleSet Dorian { get; }
    public static ScaleSet Phrygian { get; }
    public static ScaleSet Lydian { get; }
    public static ScaleSet Mixolydian { get; }
    public static ScaleSet Aeolian { get; }
    public static ScaleSet Locrian { get; }
}