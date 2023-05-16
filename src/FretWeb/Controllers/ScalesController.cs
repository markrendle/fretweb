using FretWeb.Models;
using FretWeb.Music;
using Microsoft.AspNetCore.Mvc;

namespace FretWeb.Controllers;

[Route("scales")]
public class ScalesController : Controller
{
    [HttpGet("{id}")]
    public IActionResult Get(string id, [FromQuery] string? force = null)
    {
        var scaleSet = Scales.FindById(id);
        
        if (scaleSet == null) return NotFound();

        var scales = scaleSet.Enumerate().ToArray();

        if (force is { Length: > 0 } && Enum.TryParse(force, true, out Sign sign))
        {
            if (sign == Sign.Sharp)
            {
                for (int i = 0; i < scales.Length; i++)
                {
                    scales[i] = Normalizer.ForceSharp(scales[i]);
                }
            }
            else if (sign == Sign.Flat)
            {
                for (int i = 0; i < scales.Length; i++)
                {
                    scales[i] = Normalizer.ForceFlat(scales[i]);
                }
            }
        }
        
        return View(new ScalesViewModel(id, scaleSet.Name, scales));
    }
}