namespace FretWeb.Music.NoteTypes;

public sealed class EFlatFlat : Note
{
    public static EFlatFlat Instance { get; } = new ();
    private EFlatFlat() { }
    
    public override int Value => 2;
    public override char Letter => 'E';
    public override Sign Sign => Sign.FlatFlat;
    public override string Display => DisplayStrings.EFlatFlat;
    public override Note Alt => Notes.D;

    public override Note AddSemitone() => Notes.EFlat;
    public override Note SubtractSemitone() => Notes.DFlat;
}