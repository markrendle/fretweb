namespace FretWeb.Music.NoteTypes;

public sealed class BFlatFlat : Note
{
    public static BFlatFlat Instance { get; } = new ();
    private BFlatFlat() { }
    
    public override int Value => 9;
    public override char Letter => 'B';
    public override Sign Sign => Sign.FlatFlat;
    public override string Display => DisplayStrings.BFlatFlat;
    public override Note Alt => Notes.A;

    public override Note AddSemitone() => Notes.BFlat;
    public override Note SubtractSemitone() => Notes.AFlat;
}