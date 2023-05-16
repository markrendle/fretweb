namespace FretWeb.Music.NoteTypes;

public sealed class GFlatFlat : Note
{
    public static GFlatFlat Instance { get; } = new ();
    private GFlatFlat() { }
    
    public override int Value => 5;
    public override char Letter => 'G';
    public override Sign Sign => Sign.FlatFlat;
    public override string Display => DisplayStrings.GFlatFlat;
    public override Note Alt => Notes.F;

    public override Note AddSemitone() => Notes.GFlat;
    public override Note SubtractSemitone() => Notes.FFlat;
}