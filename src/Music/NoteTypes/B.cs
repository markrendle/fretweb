namespace FretWeb.Music.NoteTypes;

public sealed class B : Note
{
    public override int Value => 11;
    public override char Letter => 'B';
    public override Sign Sign => Sign.Natural;
    public override string Display => DisplayStrings.B;
    public override Note Alt => Notes.CFlat;

    public override Note AddSemitone() => Notes.C;
    public override Note SubtractSemitone() => Notes.BFlat;
}
