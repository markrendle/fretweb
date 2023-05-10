namespace FretWeb.Music.NoteTypes;

public sealed class ASharp : Note
{
    public override char Letter => 'A';
    public override Sign Sign => Sign.Sharp;
    public override string Display => DisplayStrings.ASharp;
    public override Note Alt => Notes.BFlat;

    public override Note AddSemitone() => Notes.B;
}