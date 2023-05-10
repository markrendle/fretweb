namespace FretWeb.Music.NoteTypes;

public sealed class CFlat : Note
{
    public override char Letter => 'C';
    public override Sign Sign => Sign.Flat;
    public override string Display => DisplayStrings.CFlat;
    public override Note Alt => Notes.B;

    public override Note AddSemitone() => Notes.C;
}