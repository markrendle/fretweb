namespace FretWeb.Music.NoteTypes;

public sealed class BSharp : Note
{
    public static BSharp Instance { get; } = new ();
    private BSharp() { }
    
    public override int Value => 0;
    public override char Letter => 'B';
    public override Sign Sign => Sign.Sharp;
    public override string Display => DisplayStrings.BSharp;
    public override Note Alt => Notes.C;
    public override bool IsTheoretical => true;

    public override Note AddSemitone() => Notes.CSharp;
    public override Note SubtractSemitone() => Notes.B;
}