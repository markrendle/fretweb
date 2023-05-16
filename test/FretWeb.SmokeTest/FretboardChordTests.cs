using FretWeb.Controllers;
using FretWeb.Fretboards;
using FretWeb.Music;
using Microsoft.AspNetCore.Mvc.Testing;

namespace FretWeb.SmokeTest;

public class FretboardChordTests : IClassFixture<WebApplicationFactory<FretboardsController>>
{
    private readonly WebApplicationFactory<FretboardsController> _factory;

    public FretboardChordTests(WebApplicationFactory<FretboardsController> factory)
    {
        _factory = factory;
    }

    [Theory]
    [MemberData(nameof(TuningsAndChords))]
    public async Task GetsTuningChords(string tuning, string chord)
    {
        var client = _factory.CreateClient();
        var request = new HttpRequestMessage(HttpMethod.Get, $"/fretboards/{tuning}/chord/{chord}");
        var response = await client.SendAsync(request);
        Assert.True(response.IsSuccessStatusCode);
    }

    public static IEnumerable<object[]> TuningsAndChords()
    {
        foreach (var standardTuning in StandardTunings.All())
        {
            foreach (var arpeggio in Arpeggios.All().Where(a => a.Count < 6))
            {
                foreach (var note in Notes.ChromaticWithFlats())
                {
                    yield return new object[] { standardTuning.Tuning, $"{note.Id}-{arpeggio.Id}" };
                }
            }
        }
    }
}