namespace FretWeb.Music.NoteTypes;

public sealed class ESharp : Note
{
    public override char Letter => 'E';
    public override Sign Sign => Sign.Sharp;
    public override string Display => DisplayStrings.ESharp;
    public override Note Alt => Notes.F;
}