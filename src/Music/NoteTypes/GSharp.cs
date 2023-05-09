namespace FretWeb.Music.NoteTypes;

public sealed class GSharp : Note
{
    public override char Letter => 'G';
    public override Sign Sign => Sign.Sharp;
    public override string Display => DisplayStrings.GSharp;
    public override Note Alt => Notes.AFlat;
}