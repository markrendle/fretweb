namespace FretWeb.Music.NoteTypes;

public sealed class CSharp : Note
{
    public override char Letter => 'C';
    public override Sign Sign => Sign.Sharp;
    public override string Display => DisplayStrings.CSharp;
    public override Note Alt => Notes.DFlat;

    public override Note AddSemitone() => Notes.D;
}