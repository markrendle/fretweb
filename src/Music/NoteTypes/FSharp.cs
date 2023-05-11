namespace FretWeb.Music.NoteTypes;

public sealed class FSharp : Note
{
    public static FSharp Instance { get; } = new ();
    private FSharp() { }
    
    public override int Value => 6;
    public override char Letter => 'F';
    public override Sign Sign => Sign.Sharp;
    public override string Display => DisplayStrings.FSharp;
    public override Note Alt => Notes.GFlat;

    public override Note AddSemitone() => Notes.G;
    public override Note SubtractSemitone() => Notes.F;
}