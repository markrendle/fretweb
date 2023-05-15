using FretWeb.Fretboards;
using FretWeb.Models;
using FretWeb.Music;
using FretWeb.Music.NoteTypes;
using Microsoft.AspNetCore.Mvc;

namespace FretWeb.Controllers;

[Route("print")]
public class PrintController : Controller
{
    [HttpGet("{tuning}/scale/{scales}")]
    public IActionResult Scale(string tuning, string scales, [FromQuery] int? frets)
    {
        var tuningArray = ParseTuningArray(tuning);
        var scalesArray = scales.Split('+');
        var viewModel = new PrintViewModel();
        foreach (var scaleStr in scalesArray)
        {
            var parts = scaleStr.Split('-');
            if (parts.Length != 2)
            {
                return NotFound();
            }

            if (parts[0] is not { Length: > 0 } root || !Note.TryParse(root, out var rootNote))
            {
                return NotFound();
            }

            if (parts[1] is not { Length: > 0 } scaleName || !(Scales.FindByName(scaleName) is { } scaleSet) || !scaleSet.TryGet(rootNote, out var scale))
            {
                return NotFound();
            }

            var fretboard = Fretboard.Create(frets ?? 12, tuningArray);
            fretboard.SetBadges(scale, rootNote.Sign);

            var title = $"{rootNote.Display} {scaleSet.Name}";

            viewModel.Fretboards.Add(new FretboardViewModel(fretboard, title));
        }
        return View(viewModel);
    }
    
    private static Note[] ParseTuningArray(string tuning)
    {
        var list = new List<Note>();
        var notes = tuning.AsSpan();
        for (int i = 0; i < notes.Length; i++)
        {
            var n = notes[i];
            if (!char.IsUpper(n) || n is < 'A' or > 'G') continue;
            var sign = Sign.Natural;
            if (i + 1 < notes.Length)
            {
                var s = notes[i + 1];
                if (s == 'f')
                {
                    sign = Sign.Flat;
                    i++;
                }
                else if (s == 's')
                {
                    sign = Sign.Sharp;
                    i++;
                }
            }

            list.Add(Notes.Get(n, sign));
        }

        var tuningArray = list.ToArray();
        return tuningArray;
    }
}