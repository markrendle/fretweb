namespace FretWeb.Models;

public class ArpeggiosBuilderViewModel
{
    public string? Tuning { get; set; }
    public RootedArpeggio[] Arpeggios { get; set; }
}