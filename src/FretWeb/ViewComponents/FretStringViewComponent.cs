using FretWeb.Fretboards;
using Microsoft.AspNetCore.Mvc;

namespace FretWeb.ViewComponents;

public class FretStringViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(FretString fretString, string backgroundClass)
    {
        ViewData["backgroundClass"] = backgroundClass;
        ViewData["index"] = fretString.Index.HasValue ? fretString.Index.Value.ToString() : string.Empty;
        ViewData["cls"] = fretString.IsRoot ? "fret-badge fret-badge-root" : "fret-badge";
        return View(fretString);
    }
}