namespace FretWeb.Music.NoteTypes;

public sealed class EFlat : Note
{
    public static EFlat Instance { get; } = new ();
    private EFlat() { }
    
    public override int Value => 3;
    public override char Letter => 'E';
    public override Sign Sign => Sign.Flat;
    public override string Display => DisplayStrings.EFlat;
    public override Note Alt => Notes.DSharp;

    public override Note AddSemitone() => Notes.E;
    public override Note SubtractSemitone() => Notes.D;
}