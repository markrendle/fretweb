namespace FretWeb.Music.NoteTypes;

public sealed class CSharpSharp : Note
{
    public static CSharpSharp Instance { get; } = new ();
    private CSharpSharp() { }
    
    public override int Value => 2;
    public override char Letter => 'C';
    public override Sign Sign => Sign.SharpSharp;
    public override string Display => DisplayStrings.CSharpSharp;
    public override Note Alt => Notes.D;

    public override Note AddSemitone() => Notes.DSharp;
    public override Note SubtractSemitone() => Notes.CSharp;
}