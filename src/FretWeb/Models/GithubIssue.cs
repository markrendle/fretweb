namespace FretWeb.Models;

public class GithubIssue
{
    public string Title { get; set; }
    public string Body { get; set; }
    public string[] Assignees { get; set; } = new[] { "markrendle" };
    public string[] Labels { get; set; } = new[] { "website" };
}