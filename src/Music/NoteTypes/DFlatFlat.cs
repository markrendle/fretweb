namespace FretWeb.Music.NoteTypes;

public sealed class DFlatFlat : Note
{
    public static DFlatFlat Instance { get; } = new ();
    private DFlatFlat() { }
    
    public override int Value => 0;
    public override char Letter => 'D';
    public override Sign Sign => Sign.FlatFlat;
    public override string Display => DisplayStrings.DFlatFlat;
    public override Note Alt => Notes.BSharp;

    public override Note AddSemitone() => Notes.DFlat;
    public override Note SubtractSemitone() => Notes.B;
}