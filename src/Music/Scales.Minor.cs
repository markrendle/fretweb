namespace FretWeb.Music;

public static partial class Scales
{
    public static ScaleSet Minor { get; } = new(
        new Scale(Notes.C, Notes.D, Notes.EFlat, Notes.F, Notes.G, Notes.AFlat, Notes.B),
        new Scale(Notes.DFlat, Notes.EFlat, Notes.FFlat, Notes.GFlat, Notes.AFlat, Notes.BFlat, Notes.CFlat),
        new Scale(Notes.D, Notes.E, Notes.F, Notes.G, Notes.A, Notes.BFlat, Notes.C),
        new Scale(Notes.EFlat, Notes.F, Notes.GFlat, Notes.AFlat, Notes.BFlat, Notes.CFlat, Notes.DFlat),
        new Scale(Notes.E, Notes.FSharp, Notes.G, Notes.A, Notes.B, Notes.C, Notes.D),
        new Scale(Notes.F, Notes.G, Notes.AFlat, Notes.BFlat, Notes.C, Notes.DFlat, Notes.EFlat),
        new Scale(Notes.GFlat, Notes.AFlat, Notes.A, Notes.CFlat, Notes.DFlat, Notes.D, Notes.FFlat),
        new Scale(Notes.G, Notes.A, Notes.BFlat, Notes.C, Notes.D, Notes.EFlat, Notes.F),
        new Scale(Notes.AFlat, Notes.BFlat, Notes.CFlat, Notes.DFlat, Notes.EFlat, Notes.FFlat, Notes.GFlat),
        new Scale(Notes.A, Notes.B, Notes.C, Notes.D, Notes.E, Notes.F, Notes.G),
        new Scale(Notes.BFlat, Notes.C, Notes.DFlat, Notes.EFlat, Notes.F, Notes.GFlat, Notes.AFlat),
        new Scale(Notes.B, Notes.CSharp, Notes.DSharp, Notes.E, Notes.FSharp, Notes.GSharp, Notes.ASharp)
    );

    public static ScaleSet Aeolian => Minor;
}