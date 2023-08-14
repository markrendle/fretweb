using FretWeb.Models;
using FretWeb.Music;
using FretWeb.Music.NoteTypes;
using Microsoft.AspNetCore.Mvc;

namespace FretWeb.Controllers;

[Route("key")]
public class KeyController : Controller
{
    private static readonly KeyIndexViewModel IndexViewModel = new();
    // GET
    public IActionResult Index()
    {
        return View(IndexViewModel);
    }

    [HttpGet("{noteStr}/{tone}")]
    public IActionResult Detail(string noteStr, string tone)
    {
        if (!Note.TryParse(noteStr, out var note)) return NotFound();

        Key key;
        if (tone.Equals("major", StringComparison.OrdinalIgnoreCase))
        {
            key = Keys.Major.Get(note);
        }
        else if (tone.Equals("minor", StringComparison.OrdinalIgnoreCase))
        {
            key = Keys.Minor.Get(note);
        }
        else
        {
            return NotFound();
        }

        var keyViewModel = new KeyViewModel(key);
        return View(keyViewModel);
    }
}