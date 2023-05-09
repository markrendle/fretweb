using FretWeb.Music;

namespace Music.Tests;

public class Equivalences
{
    [Fact]
    public void AreCorrect()
    {
        Assert.True(Notes.CFlat.IsEquivalentTo(Notes.B));
        Assert.True(Notes.C.IsEquivalentTo(Notes.BSharp));
        Assert.True(Notes.CSharp.IsEquivalentTo(Notes.DFlat));
        Assert.True(Notes.DFlat.IsEquivalentTo(Notes.CSharp));
        Assert.True(Notes.DSharp.IsEquivalentTo(Notes.EFlat));
        Assert.True(Notes.EFlat.IsEquivalentTo(Notes.DSharp));
        Assert.True(Notes.E.IsEquivalentTo(Notes.FFlat));
        Assert.True(Notes.ESharp.IsEquivalentTo(Notes.F));
        Assert.True(Notes.FFlat.IsEquivalentTo(Notes.E));
        Assert.True(Notes.F.IsEquivalentTo(Notes.ESharp));
        Assert.True(Notes.FSharp.IsEquivalentTo(Notes.GFlat));
        Assert.True(Notes.GFlat.IsEquivalentTo(Notes.FSharp));
        Assert.True(Notes.GSharp.IsEquivalentTo(Notes.AFlat));
        Assert.True(Notes.AFlat.IsEquivalentTo(Notes.GSharp));
        Assert.True(Notes.ASharp.IsEquivalentTo(Notes.BFlat));
        Assert.True(Notes.BFlat.IsEquivalentTo(Notes.ASharp));
        Assert.True(Notes.B.IsEquivalentTo(Notes.CFlat));
        Assert.True(Notes.BSharp.IsEquivalentTo(Notes.C));
    }
}