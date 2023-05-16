namespace FretWeb.Music.NoteTypes;

public sealed class DSharpSharp : Note
{
    public static DSharpSharp Instance { get; } = new ();
    private DSharpSharp() { }
    
    public override int Value => 4;
    public override char Letter => 'D';
    public override Sign Sign => Sign.SharpSharp;
    public override string Display => DisplayStrings.DSharpSharp;
    public override Note Alt => Notes.FFlat;

    public override Note AddSemitone() => Notes.ESharp;
    public override Note SubtractSemitone() => Notes.DSharp;
}