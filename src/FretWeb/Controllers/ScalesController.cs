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
            "minor" => Scales.Minor,
            "ionian" => Scales.Ionian,
            "dorian" => Scales.Dorian,
            "phrygian" => Scales.Phrygian,
            "lydian" => Scales.Lydian,
            "mixolydian" => Scales.Mixolydian,
            "aeolian" => Scales.Aeolian,
            "locrian" => Scales.Locrian,
            _ => null
        };
        
        if (scaleSet == null) return NotFound();
        
        name = name switch
        {
            "major" => "Major",
            "minor" => "Minor",
            "ionian" => "Ionian",
            "dorian" => "Dorian",
            "phrygian" => "Phrygian",
            "lydian" => "Lydian",
            "mixolydian" => "Mixolydian",
            "aeolian" => "Aeolian",
            "locrian" => "Locrian",
            _ => throw new ArgumentException()
        };

        return View(new ScalesViewModel(name, scaleSet));
    }
}