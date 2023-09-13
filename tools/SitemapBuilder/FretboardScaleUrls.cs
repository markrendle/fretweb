using FretWeb.Fretboards;
using FretWeb.Music;

namespace SitemapBuilder;

public static class FretboardScaleUrls
{
    public static IEnumerable<string> Get()
    {
        foreach (var tuning in StandardTunings.All().Select(t => t.Tuning))
        {
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