namespace FretWeb.Models;

public class ScalesBuilderViewModel
{
    public string? Tuning { get; set; }
    public BuilderScale[] Scales { get; set; }
}

public class BuilderScale
{
    public string? Root { get; set; }
    public string? Scale { get; set; }
}