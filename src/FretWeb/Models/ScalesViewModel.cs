using FretWeb.Music;

namespace FretWeb.Models;

public class ScalesViewModel
{
    public ScalesViewModel(string id, string name, Scale[] scales, Sign forceSign = Sign.Natural)
    {
        Id = id;
        Name = name;
        Scales = scales;
        ForceSign = forceSign;
    }

    public string Id { get; set; }
    public string Name { get; }
    public Scale[] Scales { get; }
    public Sign ForceSign { get; }
}