namespace FretWeb.Music.NoteTypes;

public sealed class E : Note
{
    public static E Instance { get; } = new ();
    private E() { }
    
    public override int Value => 4;
    public override char Letter => 'E';
    public override Sign Sign => Sign.Natural;
    public override string Display => DisplayStrings.E;
    public override Note Alt => Notes.FFlat;

    public override Note AddSemitone() => Notes.F;
    public override Note SubtractSemitone() => Notes.EFlat;
    public override Note AsFlat() => FFlat.Instance;
    public override Note AsSharp() => DSharpSharp.Instance;
}