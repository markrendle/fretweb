namespace FretWeb.Music.NoteTypes;

public sealed class EFlat : Note
{
    public override char Letter => 'E';
    public override Sign Sign => Sign.Flat;
    public override string Display => DisplayStrings.EFlat;
    public override Note Alt => Notes.DSharp;

    public override Note AddSemitone() => Notes.E;
}