namespace FretWeb.Music.NoteTypes;

public sealed class F : Note
{
    public override char Letter => 'F';
    public override Sign Sign => Sign.Natural;
    public override string Display => DisplayStrings.F;
    public override Note Alt => Notes.ESharp;
}