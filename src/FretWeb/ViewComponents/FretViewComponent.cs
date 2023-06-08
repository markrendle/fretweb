using FretWeb.Fretboards;
using Microsoft.AspNetCore.Mvc;

namespace FretWeb.ViewComponents;

public class FretViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(Fret fret)
    {
      string inlay;
      if (fret.Number is 12 or 24)
      {
        inlay = "⬤ ⬤";
      }
      else if (fret.Number % 12 is 3 or 5 or 7 or 9)
      {
        inlay = "⬤";
      }
      else
      {
        inlay = string.Empty;
      }

      ViewData["inlay"] = inlay;
      return View(fret);
    }
}