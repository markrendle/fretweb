namespace FretWeb.Models;

public class ScalesBuilderViewModel
{
    public string? Tuning { get; init; }
    public required RootedScale[] Scales { get; init; }
}