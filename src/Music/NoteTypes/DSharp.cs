namespace FretWeb.Music.NoteTypes;

public sealed class DSharp : Note
{
    public override char Letter => 'D';
    public override Sign Sign => Sign.Sharp;
    public override string Display => DisplayStrings.DSharp;
    public override Note Alt => Notes.EFlat;
}