using FretWeb.Controllers;
using FretWeb.Fretboards;
using FretWeb.Music;
using Microsoft.AspNetCore.Mvc.Testing;

namespace FretWeb.SmokeTest;

public class FretboardNoteTests : IClassFixture<WebApplicationFactory<FretboardsController>>
{
    private readonly WebApplicationFactory<FretboardsController> _factory;

    public FretboardNoteTests(WebApplicationFactory<FretboardsController> factory)
    {
        _factory = factory;
    }

    [Theory]
    [MemberData(nameof(TuningsAndNotes))]
    public async Task GetsTuningScale(string tuning, string note)
    {
        var client = _factory.CreateClient();
        var request = new HttpRequestMessage(HttpMethod.Get, $"/fretboards/{tuning}/note/{note}");
        var response = await client.SendAsync(request);
        Assert.True(response.IsSuccessStatusCode);
    }

    [Theory]
    [MemberData(nameof(TuningsAndNotes))]
    public async Task GetsTuningScaleForPrint(string tuning, string note)
    {
        var client = _factory.CreateClient();
        var request = new HttpRequestMessage(HttpMethod.Get, $"/fretboards/{tuning}/note/{note}?print=true");
        var response = await client.SendAsync(request);
        Assert.True(response.IsSuccessStatusCode);
    }

    public static IEnumerable<object[]> TuningsAndNotes()
    {
        foreach (var tuning in AllTunings.Get())
        {
            foreach (var letter in new[]{'C', 'D', 'E', 'F', 'G', 'A', 'B'})
            {
                yield return new object[] { tuning, $"{letter}" };
                yield return new object[] { tuning, $"{letter}f" };
                yield return new object[] { tuning, $"{letter}s" };
            }
        }
    }
}

internal static class AllTunings
{
    private static readonly string[] All = StandardTunings.All().Select(t => t.Tuning).Distinct().ToArray();

    public static IEnumerable<string> Get() => All.AsEnumerable();
}