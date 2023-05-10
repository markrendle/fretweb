namespace FretWeb.Music.NoteTypes;

public sealed class C : Note
{
    public override char Letter => 'C';
    public override Sign Sign => Sign.Natural;
    public override string Display => DisplayStrings.C;
    public override Note Alt => Notes.BSharp;

    public override Note AddSemitone() => Notes.CSharp;
}