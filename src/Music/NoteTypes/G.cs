namespace FretWeb.Music.NoteTypes;

public sealed class G : Note
{
    public static G Instance { get; } = new ();
    private G() { }
    
    public override int Value => 7;
    public override char Letter => 'G';
    public override Sign Sign => Sign.Natural;
    public override string Display => DisplayStrings.G;
    public override Note Alt => this;

    public override Note AddSemitone() => Notes.GSharp;
    public override Note SubtractSemitone() => Notes.GFlat;
    public override Note AsFlat() => AFlatFlat.Instance;
    public override Note AsSharp() => FSharpSharp.Instance;
}