using System.Text;
using FretWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace FretWeb.Controllers;

[Route("builder")]
public class BuilderController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet("scales")]
    public IActionResult Scales([FromQuery] string? tuning)
    {
        var scales = new BuilderScale[12];
        scales[0] = new BuilderScale
        {
            Root = "C",
            Scale = "Major"
        };
        var viewModel = new ScalesBuilderViewModel
        {
            Tuning = tuning ?? "EADG",
            Scales = scales
        };
        return View(viewModel);
    }

    [HttpPost("scales")]
    public IActionResult BuildScales([FromForm] ScalesBuilderViewModel model)
    {
        var scales = new List<string>();
        foreach (var builderScale in model.Scales)
        {
            if (builderScale.Root is { Length: > 0 } root && builderScale.Scale is { Length: > 0 } scale)
            {
                scales.Add($"{root}-{scale}");
            }
        }

        if (scales.Count == 0)
        {
            return RedirectToAction("Scales", new { tuning = model.Tuning });
        }

        return RedirectToAction("Scale", "Fretboards", new { openNotes = model.Tuning, scales = string.Join('+', scales) });
    }
}