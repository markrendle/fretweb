using FretWeb.Fretboards;
using Microsoft.AspNetCore.Mvc;

namespace FretWeb.ViewComponents;

public class FretboardPrefixViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(Fret fret) => View(fret);
}