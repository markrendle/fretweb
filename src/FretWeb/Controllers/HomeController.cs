using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using FretWeb.Models;
using Microsoft.AspNetCore.OutputCaching;

namespace FretWeb.Controllers;

public class HomeController : Controller
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<HomeController> _logger;

    public HomeController(HttpClient httpClient, ILogger<HomeController> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public IActionResult Index()
    {
        var model = FretboardIndexViewModel.Instance;
        return View(model);
    }

    [HttpGet("feedback")]
    public IActionResult Feedback()
    {
        // If we don't have the GitHub token we can't send feedback
        var feedbackViewModel = new FeedbackViewModel
        {
            IsUnavailable = Environment.GetEnvironmentVariable("GITHUB_TOKEN") is not { Length: > 0 }
        };
        return View(feedbackViewModel);
    }

    [HttpPost("feedback")]
    public async Task<IActionResult> PostFeedback([FromForm] FeedbackViewModel model)
    {
        if (string.IsNullOrWhiteSpace(model.Feedback)) return RedirectToAction(nameof(Feedback));

        if (Environment.GetEnvironmentVariable("GITHUB_TOKEN") is not { Length: > 0 } githubToken)
        {
            return RedirectToAction(nameof(ThankYouForFeedback), new { failed = true });
        }

        var issue = new GithubIssue
        {
            Title = $"Feedback {DateTimeOffset.UtcNow:yyyy-MM-dd HH:mm}",
            Body = model.Feedback
        };

        var json = JsonSerializer.Serialize(issue, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        var request = new HttpRequestMessage(HttpMethod.Post, "https://api.github.com/repos/markrendle/fretweb/issues");
        request.Content = new StringContent(json, Encoding.UTF8, "application/json");
        request.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/vnd.github+json"));
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", githubToken);
        request.Headers.Add("X-GitHub-Api-Version", "2022-11-28");
        request.Headers.UserAgent.Add(new ProductInfoHeaderValue("FretWeb", "1.0"));

        var response = await _httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            var content = response.Content.ReadAsStringAsync();
            _logger.LogError("GitHub issue creation failed with status code {StatusCode}", (int)response.StatusCode);
            return RedirectToAction(nameof(ThankYouForFeedback), new { failed = true });
        }
        
        return RedirectToAction(nameof(ThankYouForFeedback));
    }

    [HttpGet("feedback/thankyou")]
    public IActionResult ThankYouForFeedback([FromQuery] bool? failed)
    {
        return View(new ThankYouForFeedbackViewModel{ Failed = failed.HasValue && failed.Value });
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}