namespace FretWeb.Music.NoteTypes;

public sealed class DSharp : Note
{
    public static DSharp Instance { get; } = new ();
    private DSharp() { }
    
    public override int Value => 3;
    public override char Letter => 'D';
    public override Sign Sign => Sign.Sharp;
    public override string Display => DisplayStrings.DSharp;
    public override Note Alt => Notes.EFlat;

    public override Note AddSemitone() => Notes.E;
    public override Note SubtractSemitone() => Notes.D;
}