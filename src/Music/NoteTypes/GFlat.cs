namespace FretWeb.Music.NoteTypes;

public sealed class GFlat : Note
{
    public override char Letter => 'G';
    public override Sign Sign => Sign.Flat;
    public override string Display => DisplayStrings.GFlat;
    public override Note Alt => Notes.FSharp;
}