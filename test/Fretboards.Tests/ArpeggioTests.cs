using FretWeb.Fretboards;
using FretWeb.Music;
using FretWeb.Music.NoteTypes;

namespace Fretboards.Tests;

public class ArpeggioTests
{
    private static readonly string Flat5 = $"{Sign.Flat.GetString()}5";
    
    [Fact]
    public void CMajor()
    {
        var fretboard = Fretboard.Create(12, Notes.E, Notes.A, Notes.D, Notes.G);
        Assert.True(Arpeggios.TryGet("Major", out var arpeggio));
        
        var c = fretboard.GetFirst(Notes.C);
        var e = fretboard.GetFirst(Notes.E);
        var g = fretboard.GetFirst(Notes.G);
        
        Assert.Equal("C", c.Badge);
        Assert.Equal("E", e.Badge);
        Assert.Equal("G", g.Badge);
        
        fretboard.SetBadges(arpeggio, Notes.C);
        
        Assert.Equal("C", c.Badge);
        Assert.Equal("3", e.Badge);
        Assert.Equal("5", g.Badge);
    }
    
    [Fact]
    public void CMajor7()
    {
        var fretboard = Fretboard.Create(12, Notes.E, Notes.A, Notes.D, Notes.G);
        Assert.True(Arpeggios.TryGet("Major7th", out var arpeggio));
        
        var root = fretboard.GetFirst(Notes.C);
        var three = fretboard.GetFirst(Notes.E);
        var five = fretboard.GetFirst(Notes.G);
        var seven = fretboard.GetFirst(Notes.B);
        
        fretboard.SetBadges(arpeggio, Notes.C);
        Assert.Equal("C", root.Badge);
        Assert.Equal("3", three.Badge);
        Assert.Equal("5", five.Badge);
        Assert.Equal("7", seven.Badge);
    }
    
    [Fact]
    public void GMajor7()
    {
        var fretboard = Fretboard.Create(12, Notes.E, Notes.A, Notes.D, Notes.G);
        Assert.True(Arpeggios.TryGet("Major7th", out var arpeggio));
        
        var root = fretboard.GetFirst(Notes.G);
        var three = fretboard.GetFirst(Notes.B);
        var five = fretboard.GetFirst(Notes.D);
        var seven = fretboard.GetFirst(Notes.FSharp);
        
        fretboard.SetBadges(arpeggio, Notes.G);
        Assert.Equal("G", root.Badge);
        Assert.Equal("3", three.Badge);
        Assert.Equal("5", five.Badge);
        Assert.Equal("7", seven.Badge);
    }
    
    [Fact]
    public void CMajor7Flat5()
    {
        var fretboard = Fretboard.Create(12, Notes.E, Notes.A, Notes.D, Notes.G);
        Assert.True(Arpeggios.TryGet("Major7thflat5", out var arpeggio));
        
        var c = fretboard.GetFirst(Notes.C);
        var e = fretboard.GetFirst(Notes.E);
        var gFlat = fretboard.GetFirst(Notes.GFlat);
        var b = fretboard.GetFirst(Notes.B);
        
        fretboard.SetBadges(arpeggio, Notes.C);
        
        Assert.Equal("C", c.Badge);
        Assert.Equal("3", e.Badge);
        Assert.Equal(Flat5, gFlat.Badge);
        Assert.Equal("7", b.Badge);
    }
    
    [Fact]
    public void CMinor()
    {
        var fretboard = Fretboard.Create(12, Notes.E, Notes.A, Notes.D, Notes.G);
        Assert.True(Arpeggios.TryGet("Minor", out var arpeggio));
        
        var c = fretboard.GetFirst(Notes.C);
        var eFlat = fretboard.GetFirst(Notes.EFlat);
        var g = fretboard.GetFirst(Notes.G);
        
        Assert.Equal(Notes.C.Display, c.Badge);
        Assert.Equal(Notes.EFlat.Display, eFlat.Badge);
        Assert.Equal(Notes.G.Display, g.Badge);
        
        fretboard.SetBadges(arpeggio, Notes.C);
        
        Assert.Equal("C", c.Badge);
        Assert.Equal("♭3", eFlat.Badge);
        Assert.Equal("5", g.Badge);
    }
}

internal static class FretboardTestExtensions
{
    public static FretString GetFirst(this Fretboard fretboard, Note note)
    {
        foreach (var fret in fretboard.Frets)
        {
            foreach (var fretString in fret.Strings)
            {
                if (fretString.Note.IsEquivalentTo(note)) return fretString;
            }
        }

        throw new InvalidOperationException();
    }
}