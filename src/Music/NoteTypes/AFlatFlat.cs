namespace FretWeb.Music.NoteTypes;

public sealed class AFlatFlat : Note
{
    public static AFlatFlat Instance { get; } = new ();
    private AFlatFlat() { }
    
    public override int Value => 7;
    public override char Letter => 'A';
    public override Sign Sign => Sign.FlatFlat;
    public override string Display => DisplayStrings.AFlatFlat;
    public override Note Alt => Notes.G;

    public override Note AddSemitone() => Notes.AFlat;
    public override Note SubtractSemitone() => Notes.GFlat;
}