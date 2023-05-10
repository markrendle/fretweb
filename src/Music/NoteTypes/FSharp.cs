namespace FretWeb.Music.NoteTypes;

public sealed class FSharp : Note
{
    public override char Letter => 'F';
    public override Sign Sign => Sign.Sharp;
    public override string Display => DisplayStrings.FSharp;
    public override Note Alt => Notes.GFlat;

    public override Note AddSemitone() => Notes.G;
}