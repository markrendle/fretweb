namespace FretWeb.Music.NoteTypes;

public sealed class E : Note
{
    public override char Letter => 'E';
    public override Sign Sign => Sign.Natural;
    public override string Display => DisplayStrings.E;
    public override Note Alt => Notes.FFlat;
}