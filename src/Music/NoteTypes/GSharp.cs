namespace FretWeb.Music.NoteTypes;

public sealed class GSharp : Note
{
    public static GSharp Instance { get; } = new ();
    private GSharp() { }
    
    public override int Value => 8;
    public override char Letter => 'G';
    public override Sign Sign => Sign.Sharp;
    public override string Display => DisplayStrings.GSharp;
    public override Note Alt => Notes.AFlat;

    public override Note AddSemitone() => Notes.A;
    public override Note SubtractSemitone() => Notes.G;
}