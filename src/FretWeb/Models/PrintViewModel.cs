using FretWeb.Fretboards;

namespace FretWeb.Models;

public class PrintViewModel
{
    public List<FretboardViewModel> Fretboards { get; } = new();
}