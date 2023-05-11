using FretWeb.Fretboards;
using FretWeb.Music;
using FretWeb.Music.NoteTypes;

namespace Fretboards.Tests;

public class FretboardTests
{
    [Fact]
    public void Builds4StringBass()
    {
        var actual = Fretboard.Create(12, Notes.E, Notes.A, Notes.D, Notes.G);
        Assert.Equal(13, actual.Frets.Count);
        
        AssertFret(actual.Frets[0],  Notes.G,     Notes.D,     Notes.A,     Notes.E);
        AssertFret(actual.Frets[1],  Notes.AFlat, Notes.EFlat, Notes.BFlat, Notes.F);
        AssertFret(actual.Frets[2],  Notes.A,     Notes.E,     Notes.B,     Notes.GFlat);
        AssertFret(actual.Frets[3],  Notes.BFlat, Notes.F,     Notes.C,     Notes.G);
        AssertFret(actual.Frets[4],  Notes.B,     Notes.GFlat, Notes.DFlat, Notes.AFlat);
        AssertFret(actual.Frets[5],  Notes.C,     Notes.G,     Notes.D,     Notes.A);
        AssertFret(actual.Frets[6],  Notes.DFlat, Notes.AFlat, Notes.EFlat, Notes.BFlat);
        AssertFret(actual.Frets[7],  Notes.D,     Notes.A,     Notes.E,     Notes.B);
        AssertFret(actual.Frets[8],  Notes.EFlat, Notes.BFlat, Notes.F,     Notes.C);
        AssertFret(actual.Frets[9],  Notes.E,     Notes.B,     Notes.GFlat, Notes.DFlat);
        AssertFret(actual.Frets[10], Notes.F,     Notes.C,     Notes.G,     Notes.D);
        AssertFret(actual.Frets[11], Notes.GFlat, Notes.DFlat, Notes.AFlat, Notes.EFlat);
        AssertFret(actual.Frets[12], Notes.G,     Notes.D,     Notes.A,     Notes.E);
    }

    private static void AssertFret(Fret fret, params Note[] notes)
    {
        Assert.Equal(notes.Length, fret.Strings.Count);
        for (int i = 0; i < notes.Length; i++)
        {
            Assert.Equal(notes[i], fret.Strings[i].Note);
        }
    }
}