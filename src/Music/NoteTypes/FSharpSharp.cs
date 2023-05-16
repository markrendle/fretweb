namespace FretWeb.Music.NoteTypes;

public sealed class FSharpSharp : Note
{
    public static FSharpSharp Instance { get; } = new ();
    private FSharpSharp() { }
    
    public override int Value => 7;
    public override char Letter => 'F';
    public override Sign Sign => Sign.SharpSharp;
    public override string Display => DisplayStrings.FSharpSharp;
    public override Note Alt => Notes.G;

    public override Note AddSemitone() => Notes.GSharp;
    public override Note SubtractSemitone() => Notes.FSharp;
}