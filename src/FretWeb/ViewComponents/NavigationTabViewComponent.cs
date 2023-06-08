using FretWeb.Models;
using FretWeb.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace FretWeb.ViewComponents;

public class NavigationTabViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(FretboardPageViewModel model, string key, string title)
    {
        ViewData["tabHref"] = Url.Fretboard(model, tab: key);
        ViewData["active"] = model.Tab.Equals(key, StringComparison.OrdinalIgnoreCase) ? "active" : string.Empty;
        ViewData["title"] = title;
        return View(model);
    }
}