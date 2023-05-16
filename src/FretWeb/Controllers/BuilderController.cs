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
        var scales = new RootedScale[12];
        scales[0] = new RootedScale
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

        return RedirectToAction("Scale", "Fretboards", new { tuning = model.Tuning, scales = string.Join('+', scales) });
    }
    
    [HttpGet("arpeggios")]
    public IActionResult Arpeggios([FromQuery] string? tuning)
    {
        var scales = new RootedArpeggio[12];
        scales[0] = new RootedArpeggio
        {
            Root = "C",
            Arpeggio = "Major"
        };
        var viewModel = new ArpeggiosBuilderViewModel()
        {
            Tuning = tuning ?? "EADG",
            Arpeggios = scales
        };
        return View(viewModel);
    }

    [HttpPost("arpeggios")]
    public IActionResult BuildArpeggios([FromForm] ArpeggiosBuilderViewModel model)
    {
        var arpeggios = new List<string>();
        foreach (var builderScale in model.Arpeggios)
        {
            if (builderScale.Root is { Length: > 0 } root && builderScale.Arpeggio is { Length: > 0 } arpeggio)
            {
                arpeggios.Add($"{root}-{arpeggio}");
            }
        }

        if (arpeggios.Count == 0)
        {
            return RedirectToAction("Arpeggios", new { tuning = model.Tuning });
        }

        return RedirectToAction("Arpeggio", "Fretboards", new { tuning = model.Tuning, arpeggios = string.Join('+', arpeggios) });
    }
}