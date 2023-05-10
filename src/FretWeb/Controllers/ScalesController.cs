using FretWeb.Models;
using FretWeb.Music;
using Microsoft.AspNetCore.Mvc;

namespace FretWeb.Controllers;

[Route("scales")]
public class ScalesController : Controller
{
    [HttpGet("{name}")]
    public IActionResult Get(string name)
    {
        name = name.ToLowerInvariant();
        
        var scaleSet = name switch
        {
            "major" => Scales.Major,
            "ionian" => Scales.Ionian,
            "minor" => Scales.Major,
            "aeolian" => Scales.Aeolian,
            "mixolydian" => Scales.Mixolydian,
            _ => null
        };
        
        if (scaleSet == null) return NotFound();
        
        name = name switch
        {
            "major" => "Major",
            "ionian" => "Ionian",
            "minor" => "Minor",
            "aeolian" => "Aeolian",
            "mixolydian" => "Mixolydian",
            _ => throw new ArgumentException()
        };

        return View(new ScalesViewModel(name, scaleSet));
    }
}