namespace FretWeb.Music.Tests;

public class ArpeggioParserTests
{
    [Fact]
    public void LoadsArpeggios()
    {
        var arpeggios = ArpeggioParser.Load().ToArray();
        Assert.NotEmpty(arpeggios);
        Assert.Contains(arpeggios, a => a.Group == "Major" && a.Name == "Major");
        Assert.Contains(arpeggios, a => a.Group == "Minor" && a.Name == $"Minor 7th ({DisplayStrings.Flat}5)");
    }
}