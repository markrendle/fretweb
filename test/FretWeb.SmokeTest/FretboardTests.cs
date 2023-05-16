using FretWeb.Controllers;
using FretWeb.Fretboards;
using Microsoft.AspNetCore.Mvc.Testing;

namespace FretWeb.SmokeTest;

public class FretboardTuningTests : IClassFixture<WebApplicationFactory<FretboardsController>>
{
    private readonly WebApplicationFactory<FretboardsController> _factory;

    public FretboardTuningTests(WebApplicationFactory<FretboardsController> factory)
    {
        _factory = factory;
    }

    [Theory]
    [MemberData(nameof(Tunings))]
    public async Task GetsTuning(string tuning)
    {
        var client = _factory.CreateClient();
        var request = new HttpRequestMessage(HttpMethod.Get, $"/fretboards/{tuning}");
        var response = await client.SendAsync(request);
        Assert.True(response.IsSuccessStatusCode);
    }

    public static IEnumerable<object[]> Tunings()
    {
        foreach (var standardTuning in StandardTunings.All())
        {
            yield return new object[] { standardTuning.Tuning };
        }
    }
}