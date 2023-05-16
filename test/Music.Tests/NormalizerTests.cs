using FretWeb.Music.NoteTypes;

namespace FretWeb.Music.Tests;

public class NormalizerTests
{
    [Fact]
    public void DFlatMinor()
    {
        var cSharpMinor = Scales.Minor.Get(Notes.CSharp);
        var actual = Normalizer.ForceFlat(cSharpMinor);
        Assert.Equal(Notes.DFlat, actual[0]);
        Assert.Equal(Notes.EFlat, actual[1]);
        Assert.Equal(Notes.FFlat, actual[2]);
        Assert.Equal(Notes.GFlat, actual[3]);
        Assert.Equal(Notes.AFlat, actual[4]);
        Assert.Equal(BFlatFlat.Instance, actual[5]);
        Assert.Equal(Notes.CFlat, actual[6]);
    }

    [Fact]
    public void GFlatMinor()
    {
        var fSharpMinor = Scales.Minor.Get(Notes.FSharp);
        var actual = Normalizer.ForceFlat(fSharpMinor);
        Assert.Equal(GFlat.Instance, actual[0]);
        Assert.Equal(AFlat.Instance, actual[1]);
        Assert.Equal(BFlatFlat.Instance, actual[2]);
        Assert.Equal(CFlat.Instance, actual[3]);
        Assert.Equal(DFlat.Instance, actual[4]);
        Assert.Equal(EFlatFlat.Instance, actual[5]);
        Assert.Equal(FFlat.Instance, actual[6]);
    }

    [Fact]
    public void AFlatMinor()
    {
        var gSharpMinor = Scales.Minor.Get(Notes.GSharp);
        var actual = Normalizer.ForceFlat(gSharpMinor);
        Assert.Equal(AFlat.Instance, actual[0]);
        Assert.Equal(BFlat.Instance, actual[1]);
        Assert.Equal(CFlat.Instance, actual[2]);
        Assert.Equal(DFlat.Instance, actual[3]);
        Assert.Equal(EFlat.Instance, actual[4]);
        Assert.Equal(FFlat.Instance, actual[5]);
        Assert.Equal(GFlat.Instance, actual[6]);
    }

    [Fact]
    public void EMinor()
    {
        var eMinor = Scales.Minor.Get(Notes.E);
        var actual = Normalizer.ForceFlat(eMinor);
        Assert.Equal(FFlat.Instance, actual[0]);
    }
}