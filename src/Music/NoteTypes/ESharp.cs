namespace FretWeb.Music.NoteTypes;

public sealed class ESharp : Note
{
    public static ESharp Instance { get; } = new ();
    private ESharp() { }
    
    public override int Value => 5;
    public override char Letter => 'E';
    public override Sign Sign => Sign.Sharp;
    public override string Display => DisplayStrings.ESharp;
    public override Note Alt => Notes.F;
    public override bool IsTheoretical => true;

    public override Note AddSemitone() => Notes.FSharp;
    public override Note SubtractSemitone() => Notes.E;
}