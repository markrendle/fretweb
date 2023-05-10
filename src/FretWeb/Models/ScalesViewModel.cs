using FretWeb.Music;

namespace FretWeb.Models;

public class ScalesViewModel
{
    public ScalesViewModel(string name, ScaleSet scaleSet)
    {
        Name = name;
        ScaleSet = scaleSet;
    }

    public string Name { get; }
    public ScaleSet ScaleSet { get; }
}