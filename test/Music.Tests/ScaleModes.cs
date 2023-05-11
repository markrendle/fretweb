namespace FretWeb.Music.Tests;

public class ScaleModes
{
    [Fact]
    public void CMajorIsDDorian()
    {
        var actual = Scales.Dorian.Get(Notes.D);
        Assert.Equal(Notes.D, actual[0]);
        Assert.Equal(Notes.E, actual[1]);
        Assert.Equal(Notes.F, actual[2]);
        Assert.Equal(Notes.G, actual[3]);
        Assert.Equal(Notes.A, actual[4]);
        Assert.Equal(Notes.B, actual[5]);
        Assert.Equal(Notes.C, actual[6]);
    }
    
    [Fact]
    public void CMajorIsEPhrygian()
    {
        var actual = Scales.Phrygian.Get(Notes.E);
        Assert.Equal(Notes.E, actual[0]);
        Assert.Equal(Notes.F, actual[1]);
        Assert.Equal(Notes.G, actual[2]);
        Assert.Equal(Notes.A, actual[3]);
        Assert.Equal(Notes.B, actual[4]);
        Assert.Equal(Notes.C, actual[5]);
        Assert.Equal(Notes.D, actual[6]);
    }
    
    [Fact]
    public void CMajorIsFLydian()
    {
        var actual = Scales.Lydian.Get(Notes.F);
        Assert.Equal(Notes.F, actual[0]);
        Assert.Equal(Notes.G, actual[1]);
        Assert.Equal(Notes.A, actual[2]);
        Assert.Equal(Notes.B, actual[3]);
        Assert.Equal(Notes.C, actual[4]);
        Assert.Equal(Notes.D, actual[5]);
        Assert.Equal(Notes.E, actual[6]);
    }
    
    [Fact]
    public void CMajorIsGMixolydian()
    {
        var actual = Scales.Mixolydian.Get(Notes.G);
        Assert.Equal(Notes.G, actual[0]);
        Assert.Equal(Notes.A, actual[1]);
        Assert.Equal(Notes.B, actual[2]);
        Assert.Equal(Notes.C, actual[3]);
        Assert.Equal(Notes.D, actual[4]);
        Assert.Equal(Notes.E, actual[5]);
        Assert.Equal(Notes.F, actual[6]);
    }
    
    [Fact]
    public void CMajorIsAAeolian()
    {
        var actual = Scales.Aeolian.Get(Notes.A);
        Assert.Equal(Notes.A, actual[0]);
        Assert.Equal(Notes.B, actual[1]);
        Assert.Equal(Notes.C, actual[2]);
        Assert.Equal(Notes.D, actual[3]);
        Assert.Equal(Notes.E, actual[4]);
        Assert.Equal(Notes.F, actual[5]);
        Assert.Equal(Notes.G, actual[6]);
    }
    
    [Fact]
    public void CMajorIsBLocrian()
    {
        var actual = Scales.Locrian.Get(Notes.B);
        Assert.Equal(Notes.B, actual[0]);
        Assert.Equal(Notes.C, actual[1]);
        Assert.Equal(Notes.D, actual[2]);
        Assert.Equal(Notes.E, actual[3]);
        Assert.Equal(Notes.F, actual[4]);
        Assert.Equal(Notes.G, actual[5]);
        Assert.Equal(Notes.A, actual[6]);
    }
}