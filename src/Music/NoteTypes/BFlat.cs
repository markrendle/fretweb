namespace FretWeb.Music.NoteTypes;

public sealed class BFlat : Note
{
    public override char Letter => 'B';
    public override Sign Sign => Sign.Flat;
    public override string Display => DisplayStrings.BFlat;
    public override Note Alt => Notes.ASharp;
}