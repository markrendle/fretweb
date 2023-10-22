using FretWeb.Models;
using FretWeb.Music;
using FretWeb.Music.NoteTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace FretWeb.Controllers;

[Route("chord-explorer")]
public class ChordExplorerController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet("{id}")]
    public IActionResult Get(string id, [FromQuery] string? root)
    {
        if (!Arpeggios.TryGet(id, out var arpeggio)) return NotFound();
        if (!Note.TryParse(root ?? "C", out var rootNote))
        {
            rootNote = Notes.C;
        }
        return View(new ChordExplorerViewModel(arpeggio, rootNote));
    }
}