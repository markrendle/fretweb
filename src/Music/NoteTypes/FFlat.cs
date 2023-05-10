namespace FretWeb.Music.NoteTypes;

public sealed class FFlat : Note
{
    public override char Letter => 'F';
    public override Sign Sign => Sign.Flat;
    public override string Display => DisplayStrings.FFlat;
    public override Note Alt => Notes.E;

    public override Note AddSemitone() => Notes.F;
}