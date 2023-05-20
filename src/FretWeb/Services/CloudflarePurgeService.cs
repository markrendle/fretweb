using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;

namespace FretWeb.Services;

public class CloudflarePurgeService : BackgroundService
{
    private readonly ILogger<CloudflarePurgeService> _logger;

    public CloudflarePurgeService(ILogger<CloudflarePurgeService> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
        using var client = new HttpClient();
        var apiToken = Environment.GetEnvironmentVariable("CLOUDFLARE_API_TOKEN");
        var zone = Environment.GetEnvironmentVariable("CLOUDFLARE_API_ZONE");
        using var request = new HttpRequestMessage(HttpMethod.Post, $"https://api.cloudflare.com/client/v4/zones/{zone}/purge_cache");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", apiToken);
        request.Content = new StringContent("""{ "purge_everything": true }""", Encoding.UTF8, "application/json");
        var response = await client.SendAsync(request, stoppingToken);
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync(stoppingToken);
            _logger.LogError("Cloudflare API error status {StatusCode} {ErrorContent}", (int)response.StatusCode, content);
        }
    }
}