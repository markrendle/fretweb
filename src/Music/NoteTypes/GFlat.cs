namespace FretWeb.Music.NoteTypes;

public sealed class GFlat : Note
{
    public override int Value => 6;
    public override char Letter => 'G';
    public override Sign Sign => Sign.Flat;
    public override string Display => DisplayStrings.GFlat;
    public override Note Alt => Notes.FSharp;

    public override Note AddSemitone() => Notes.G;
    public override Note SubtractSemitone() => Notes.F;
}