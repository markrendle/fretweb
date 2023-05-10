namespace FretWeb.Music.NoteTypes;

public sealed class AFlat : Note
{
    public override char Letter => 'A';
    public override Sign Sign => Sign.Flat;
    public override string Display => DisplayStrings.AFlat;
    public override Note Alt => Notes.GSharp;

    public override Note AddSemitone() => Notes.A;
}