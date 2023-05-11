namespace FretWeb.Music.NoteTypes;

public sealed class DFlat : Note
{
    public static DFlat Instance { get; } = new ();
    private DFlat() { }
    
    public override int Value => 1;
    public override char Letter => 'D';
    public override Sign Sign => Sign.Flat;
    public override string Display => DisplayStrings.DFlat;
    public override Note Alt => Notes.CSharp;

    public override Note AddSemitone() => Notes.D;
    public override Note SubtractSemitone() => Notes.C;
}