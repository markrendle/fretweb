namespace FretWeb.Music.Tests;

public class ScaleCollections
{
    [Fact]
    public void IndexOfWorks()
    {
        var scale = Scales.Major.Get(Notes.C);
        
        Assert.Equal(0, scale.AsSpan().IndexOf(Notes.C));
        Assert.Equal(1, scale.AsSpan().IndexOf(Notes.D));
        Assert.Equal(2, scale.AsSpan().IndexOf(Notes.E));
        Assert.Equal(3, scale.AsSpan().IndexOf(Notes.F));
        Assert.Equal(4, scale.AsSpan().IndexOf(Notes.G));
        Assert.Equal(5, scale.AsSpan().IndexOf(Notes.A));
        Assert.Equal(6, scale.AsSpan().IndexOf(Notes.B));
    }
}