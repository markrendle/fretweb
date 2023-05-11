namespace FretWeb.Music.NoteTypes;

public sealed class ASharp : Note
{
    public static ASharp Instance { get; } = new ();
    private ASharp() { }
    
    public override int Value => 10;
    public override char Letter => 'A';
    public override Sign Sign => Sign.Sharp;
    public override string Display => DisplayStrings.ASharp;
    public override Note Alt => Notes.BFlat;

    public override Note AddSemitone() => Notes.B;
    public override Note SubtractSemitone() => Notes.A;
}