namespace FretWeb.Music.NoteTypes;

public sealed class GSharpSharp : Note
{
    public static GSharpSharp Instance { get; } = new ();
    private GSharpSharp() { }
    
    public override int Value => 9;
    public override char Letter => 'G';
    public override Sign Sign => Sign.SharpSharp;
    public override string Display => DisplayStrings.GSharpSharp;
    public override Note Alt => this;

    public override Note AddSemitone() => Notes.ASharp;
    public override Note SubtractSemitone() => Notes.GSharp;
}