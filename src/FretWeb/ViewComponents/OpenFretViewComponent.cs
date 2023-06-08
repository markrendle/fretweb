using FretWeb.Fretboards;
using Microsoft.AspNetCore.Mvc;

namespace FretWeb.ViewComponents;

public class OpenFretViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(Fret fret) => View(fret);
}