using FretWeb.Music.NoteTypes;

namespace FretWeb.Music.Tests;

public class ArpeggioTests
{
    [Fact]
    public void CMajor()
    {
        Assert.True(Arpeggios.TryGet("Major", out var arpeggio));
        var notes = arpeggio.GetNotes(Notes.C);
        Assert.Equal(Notes.C, notes[0]);
        Assert.Equal(Notes.E, notes[1]);
        Assert.Equal(Notes.G, notes[2]);
    }
    
    [Fact]
    public void GMajor7()
    {
        Assert.True(Arpeggios.TryGet("Major7", out var arpeggio));
        var notes = arpeggio.GetNotes(Notes.G);
        Assert.Equal(Notes.G, notes[0]);
        Assert.Equal(Notes.B, notes[1]);
        Assert.Equal(Notes.D, notes[2]);
        Assert.Equal(Notes.FSharp, notes[3]);
    }

    [Fact]
    public void GDiminished7()
    {
        Assert.True(Arpeggios.TryGet("Diminished7", out var arpeggio));
        var notes = arpeggio.GetNotes(Notes.G);
        Assert.Equal(Notes.G, notes[0]);
        Assert.Equal(Notes.BFlat, notes[1]);
        Assert.Equal(Notes.DFlat, notes[2]);
        Assert.Equal(Notes.FFlat, notes[3]);
    }

    [Fact]
    public void DFlatMajor7()
    {
        Assert.True(Arpeggios.TryGet("Major7", out var arpeggio));
        var notes = arpeggio.GetNotes(Notes.DFlat);
        AssertNotes(notes, Notes.DFlat, Notes.F, Notes.AFlat, Notes.C);
    }

    [Fact]
    public void DFlatMinor7()
    {
        Assert.True(Arpeggios.TryGet("Minor7", out var arpeggio));
        var notes = arpeggio.GetNotes(Notes.DFlat);
        AssertNotes(notes, Notes.DFlat, Notes.FFlat, Notes.AFlat, Notes.CFlat);
    }

    [Fact]
    public void DMajor7Flat5()
    {
        Assert.True(Arpeggios.TryGet("Major7flat5", out var arpeggio));
        var notes = arpeggio.GetNotes(Notes.D);
        AssertNotes(notes, Notes.D, Notes.FSharp, Notes.AFlat, Notes.CSharp);
    }

    [Fact]
    public void DMinor7()
    {
        Assert.True(Arpeggios.TryGet("Minor7", out var arpeggio));
        var notes = arpeggio.GetNotes(Notes.D);
        AssertNotes(notes, Notes.D, Notes.F, Notes.A, Notes.C);
    }

    [Fact]
    public void DMinor7Flat5()
    {
        Assert.True(Arpeggios.TryGet("Minor7flat5", out var arpeggio));
        var notes = arpeggio.GetNotes(Notes.D);
        AssertNotes(notes, Notes.D, Notes.F, Notes.AFlat, Notes.C);
    }
    
    private static void AssertNotes(ReadOnlySpan<Note> actual, params Note[] expected)
    {
        Assert.Equal(expected.Length, actual.Length);
        for (int i = 0; i < expected.Length; i++)
        {
            Assert.Equal(expected[i], actual[i]);
        }
    }
}