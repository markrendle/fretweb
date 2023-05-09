namespace FretWeb.Music.NoteTypes;

public sealed class D : Note
{
    public override char Letter => 'D';
    public override Sign Sign => Sign.Natural;
    public override string Display => DisplayStrings.D;
    public override Note Alt => this;
}