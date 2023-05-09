namespace FretWeb.Music;

public static partial class Scales
{
    public static ScaleSet Mixolydian = new(
        new Scale(Notes.C, Notes.D, Notes.E, Notes.F, Notes.G, Notes.A, Notes.BFlat),
        new Scale(Notes.DFlat, Notes.EFlat, Notes.F, Notes.GFlat, Notes.AFlat, Notes.BFlat, Notes.CFlat),
        new Scale(Notes.D, Notes.E, Notes.FSharp, Notes.G, Notes.A, Notes.B, Notes.C),
        new Scale(Notes.EFlat, Notes.F, Notes.G, Notes.AFlat, Notes.BFlat, Notes.C, Notes.DFlat),
        new Scale(Notes.E, Notes.FSharp, Notes.GSharp, Notes.A, Notes.B, Notes.CSharp, Notes.D),
        new Scale(Notes.F, Notes.G, Notes.A, Notes.BFlat, Notes.C, Notes.D, Notes.EFlat),
        new Scale(Notes.GFlat, Notes.AFlat, Notes.BFlat, Notes.CFlat, Notes.DFlat, Notes.EFlat, Notes.FFlat),
        new Scale(Notes.G, Notes.A, Notes.B, Notes.C, Notes.D, Notes.E, Notes.F),
        new Scale(Notes.AFlat, Notes.BFlat, Notes.C, Notes.DFlat, Notes.EFlat, Notes.F, Notes.GFlat),
        new Scale(Notes.A, Notes.B, Notes.CSharp, Notes.D, Notes.E, Notes.FSharp, Notes.G),
        new Scale(Notes.BFlat, Notes.C, Notes.D, Notes.EFlat, Notes.F, Notes.G, Notes.AFlat),
        new Scale(Notes.B, Notes.CSharp, Notes.DSharp, Notes.E, Notes.FSharp, Notes.GSharp, Notes.A)
    );
}
