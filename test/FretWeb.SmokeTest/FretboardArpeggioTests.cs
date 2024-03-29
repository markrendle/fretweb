﻿using FretWeb.Controllers;
using FretWeb.Music;
using Microsoft.AspNetCore.Mvc.Testing;

namespace FretWeb.SmokeTest;

public class FretboardArpeggioTests : IClassFixture<WebApplicationFactory<FretboardsController>>
{
    private readonly WebApplicationFactory<FretboardsController> _factory;

    public FretboardArpeggioTests(WebApplicationFactory<FretboardsController> factory)
    {
        _factory = factory;
    }

    [Theory]
    [MemberData(nameof(TuningsAndArpeggios))]
    public async Task GetsTuningArpeggios(string tuning, string arpeggio)
    {
        var client = _factory.CreateClient();
        var request = new HttpRequestMessage(HttpMethod.Get, $"/fretboards/{tuning}/arpeggio/{arpeggio}");
        var response = await client.SendAsync(request);
        Assert.True(response.IsSuccessStatusCode);
    }

    [Theory]
    [MemberData(nameof(TuningsAndArpeggios))]
    public async Task GetsTuningArpeggiosForPrint(string tuning, string arpeggio)
    {
        var client = _factory.CreateClient();
        var request = new HttpRequestMessage(HttpMethod.Get, $"/fretboards/{tuning}/arpeggio/{arpeggio}?print=true");
        var response = await client.SendAsync(request);
        Assert.True(response.IsSuccessStatusCode);
    }

    public static IEnumerable<object[]> TuningsAndArpeggios()
    {
        foreach (var tuning in AllTunings.Get())
        {
            foreach (var arpeggio in Arpeggios.All())
            {
                foreach (var note in Notes.ChromaticWithFlats())
                {
                    yield return new object[] { tuning, $"{note.Id}-{arpeggio.Id}" };
                }
            }
        }
    }
}