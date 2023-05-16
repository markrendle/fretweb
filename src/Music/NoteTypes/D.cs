namespace FretWeb.Music.NoteTypes;

public sealed class D : Note
{
    public static D Instance { get; } = new ();
    private D() { }
    
    public override int Value => 2;
    public override char Letter => 'D';
    public override Sign Sign => Sign.Natural;
    public override string Display => DisplayStrings.D;
    public override Note Alt => this;

    public override Note AddSemitone() => Notes.DSharp;
    public override Note SubtractSemitone() => Notes.DFlat;
    public override Note AsFlat() => EFlatFlat.Instance;
    public override Note AsSharp() => CSharpSharp.Instance;
}