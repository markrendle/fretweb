using FretWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace FretWeb.ViewComponents;

public class NavigationSectionViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(FretboardPageViewModel model) => View(model);
}