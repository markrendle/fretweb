using FretWeb.Fretboards;

namespace SitemapBuilder;

public static class FretboardUrls
{
    public static IEnumerable<string> Get() => StandardTunings.All()
        .Select(t => $"https://www.fretbadger.com/fretboards/{t.Tuning}")
        .Distinct();
}