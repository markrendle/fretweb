using FretWeb.Music;
using FretWeb.Music.NoteTypes;

namespace Music.Tests;

public class ScaleModes
{
    [Fact]
    public void CMajorIsDDorian()
    {
        var actual = Scales.Dorian.Get(Notes.D);
        Assert.Equal(new D(), actual[0]);
        Assert.Equal(new E(), actual[1]);
        Assert.Equal(new F(), actual[2]);
        Assert.Equal(new G(), actual[3]);
        Assert.Equal(new A(), actual[4]);
        Assert.Equal(new B(), actual[5]);
        Assert.Equal(new C(), actual[6]);
    }
    
    [Fact]
    public void CMajorIsEPhrygian()
    {
        var actual = Scales.Phrygian.Get(Notes.E);
        Assert.Equal(new E(), actual[0]);
        Assert.Equal(new F(), actual[1]);
        Assert.Equal(new G(), actual[2]);
        Assert.Equal(new A(), actual[3]);
        Assert.Equal(new B(), actual[4]);
        Assert.Equal(new C(), actual[5]);
        Assert.Equal(new D(), actual[6]);
    }
    
    [Fact]
    public void CMajorIsFLydian()
    {
        var actual = Scales.Lydian.Get(Notes.F);
        Assert.Equal(new F(), actual[0]);
        Assert.Equal(new G(), actual[1]);
        Assert.Equal(new A(), actual[2]);
        Assert.Equal(new B(), actual[3]);
        Assert.Equal(new C(), actual[4]);
        Assert.Equal(new D(), actual[5]);
        Assert.Equal(new E(), actual[6]);
    }
    
    [Fact]
    public void CMajorIsGMixolydian()
    {
        var actual = Scales.Mixolydian.Get(Notes.G);
        Assert.Equal(new G(), actual[0]);
        Assert.Equal(new A(), actual[1]);
        Assert.Equal(new B(), actual[2]);
        Assert.Equal(new C(), actual[3]);
        Assert.Equal(new D(), actual[4]);
        Assert.Equal(new E(), actual[5]);
        Assert.Equal(new F(), actual[6]);
    }
    
    [Fact]
    public void CMajorIsAAeolian()
    {
        var actual = Scales.Aeolian.Get(Notes.A);
        Assert.Equal(new A(), actual[0]);
        Assert.Equal(new B(), actual[1]);
        Assert.Equal(new C(), actual[2]);
        Assert.Equal(new D(), actual[3]);
        Assert.Equal(new E(), actual[4]);
        Assert.Equal(new F(), actual[5]);
        Assert.Equal(new G(), actual[6]);
    }
    
    [Fact]
    public void CMajorIsBLocrian()
    {
        var actual = Scales.Locrian.Get(Notes.B);
        Assert.Equal(new B(), actual[0]);
        Assert.Equal(new C(), actual[1]);
        Assert.Equal(new D(), actual[2]);
        Assert.Equal(new E(), actual[3]);
        Assert.Equal(new F(), actual[4]);
        Assert.Equal(new G(), actual[5]);
        Assert.Equal(new A(), actual[6]);
    }
}