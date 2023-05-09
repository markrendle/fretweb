namespace FretWeb.Music.NoteTypes;

public sealed class B : Note
{
    public override char Letter => 'B';
    public override Sign Sign => Sign.Natural;
    public override string Display => DisplayStrings.B;
    public override Note Alt => Notes.CFlat;
}
