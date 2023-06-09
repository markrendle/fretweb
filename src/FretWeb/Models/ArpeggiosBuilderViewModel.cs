namespace FretWeb.Models;

public class ArpeggiosBuilderViewModel
{
    public string? Tuning { get; set; }
    public required RootedArpeggio[] Arpeggios { get; init; }
}