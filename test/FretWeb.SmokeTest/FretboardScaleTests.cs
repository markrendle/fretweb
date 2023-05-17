using FretWeb.Controllers;
using FretWeb.Fretboards;
using FretWeb.Music;
using Microsoft.AspNetCore.Mvc.Testing;

namespace FretWeb.SmokeTest;

public class FretboardScaleTests : IClassFixture<WebApplicationFactory<FretboardsController>>
{
    private readonly WebApplicationFactory<FretboardsController> _factory;

    public FretboardScaleTests(WebApplicationFactory<FretboardsController> factory)
    {
        _factory = factory;
    }

    [Theory]
    [MemberData(nameof(TuningsAndScales))]
    public async Task GetsTuningScale(string tuning, string scale)
    {
        var client = _factory.CreateClient();
        var request = new HttpRequestMessage(HttpMethod.Get, $"/fretboards/{tuning}/scale/{scale}");
        var response = await client.SendAsync(request);
        Assert.True(response.IsSuccessStatusCode);
    }

    [Theory]
    [MemberData(nameof(TuningsAndScales))]
    public async Task GetsTuningScaleForPrint(string tuning, string scale)
    {
        var client = _factory.CreateClient();
        var request = new HttpRequestMessage(HttpMethod.Get, $"/fretboards/{tuning}/scale/{scale}?print=true");
        var response = await client.SendAsync(request);
        Assert.True(response.IsSuccessStatusCode);
    }

    public static IEnumerable<object[]> TuningsAndScales()
    {
        foreach (var standardTuning in StandardTunings.All())
        {
            foreach (var scaleSet in Scales.Enumerate())
            {
                foreach (var scale in scaleSet.Enumerate())
                {
                    yield return new object[] { standardTuning.Tuning, $"{scale[0].Id}-{scaleSet.Id}" };
                }
            }
        }
    }
}