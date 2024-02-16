using FretWeb.Fretboards;
using FretWeb.Music;

namespace SitemapBuilder;

public static class FretboardArpeggioUrls
{
    public static IEnumerable<string> Get()
    {
        foreach (var tuning in StandardTunings.Standard().Select(t => t.Tuning))
        {
            foreach (var arpeggio in Arpeggios.All())
            {
                foreach (var note in Notes.ChromaticWithFlats())
                {
                    yield return $"https://www.fretbadger.com/fretboards/{tuning}/arpeggio/{note.Id}-{arpeggio.Id}";
                }
            }
            foreach (var scaleSet in Scales.EnumerateScales())
            {
                foreach (var scale in scaleSet.Enumerate())
                {
                    yield return $"https://www.fretbadger.com/fretboards/{tuning}/scale/{scale[0].Id}-{scaleSet.Id}";
                }
            }
        }
    }
}

public static class KeyUrls
{
    private static readonly string[] Keys =
    {
        "C/major",
        "D-flat/major",
        "D/major",
        "E-flat/major",
        "E/major",
        "F/major",
        "G-flat/major",
        "G/major",
        "A-flat/major",
        "A/major",
        "B-flat/major",
        "B/major",
        "C/minor",
        "C-sharp/minor",
        "D/minor",
        "E-flat/minor",
        "E/minor",
        "F/minor",
        "F-sharp/minor",
        "G/minor",
        "G-sharp/minor",
        "A/minor",
        "B-flat/minor",
        "B/minor",
    };
    public static IEnumerable<string> Get()
    {
        return Keys.Select(k => $"https://www.fretbadger.com/key/{k}");
    }
}