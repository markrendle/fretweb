namespace FretWeb.Music.NoteTypes;

public sealed class A : Note
{
    public override char Letter => 'A';
    public override Sign Sign => Sign.Natural;
    public override string Display => DisplayStrings.A;
    public override Note Alt => this;

    public override Note AddSemitone() => Notes.ASharp;
}