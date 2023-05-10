namespace FretWeb.Music.NoteTypes;

public sealed class G : Note
{
    public override char Letter => 'G';
    public override Sign Sign => Sign.Natural;
    public override string Display => DisplayStrings.G;
    public override Note Alt => this;

    public override Note AddSemitone() => Notes.GSharp;
}