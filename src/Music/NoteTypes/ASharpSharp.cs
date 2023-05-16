namespace FretWeb.Music.NoteTypes;

public sealed class ASharpSharp : Note
{
    public static ASharpSharp Instance { get; } = new ();
    private ASharpSharp() { }
    
    public override int Value => 11;
    public override char Letter => 'A';
    public override Sign Sign => Sign.SharpSharp;
    public override string Display => DisplayStrings.ASharpSharp;
    public override Note Alt => Notes.B;

    public override Note AddSemitone() => Notes.C;
    public override Note SubtractSemitone() => Notes.BFlat;
}