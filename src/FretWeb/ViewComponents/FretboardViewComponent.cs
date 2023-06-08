using FretWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace FretWeb.ViewComponents;

public class FretboardViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(FretboardViewModel model) => View(model);
}