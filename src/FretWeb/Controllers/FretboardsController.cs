using FretWeb.Fretboards;
using FretWeb.Models;
using FretWeb.Music;
using FretWeb.Music.NoteTypes;
using Microsoft.AspNetCore.Mvc;

namespace FretWeb.Controllers;

[Route("fretboards")]
public class FretboardsController : Controller
{
    [HttpGet("{openNotes}")]
    public IActionResult Get(string openNotes, [FromQuery] int? frets, [FromQuery] string? tab)
    {
        var openNoteArray = ParseOpenNoteArray(openNotes);

        var fretboard = Fretboard.Create(frets ?? 12, openNoteArray);

        var viewModel = new FretboardPageViewModel
        {
            OpenNotes = openNotes,
            Frets = frets,
            Tab = tab ?? "Scales"
        };
        
        viewModel.Fretboards.Add(new FretboardViewModel(fretboard, openNotes));

        return View(viewModel);
    }

    [HttpGet("{openNotes}/arpeggio/{arpeggios}")]
    public IActionResult Arpeggio(string openNotes, string arpeggios, [FromQuery] int? frets, [FromQuery] string? tab)
    {
        var openNoteArray = ParseOpenNoteArray(openNotes);
        var viewModel = new FretboardPageViewModel
        {
            OpenNotes = openNotes,
            Frets = frets,
            Tab = tab ?? "Arpeggios",
            Arpeggios = arpeggios
        };

        var arpeggioArray = arpeggios.Split('+');

        foreach (var arpeggioStr in arpeggioArray)
        {
            var parts = arpeggioStr.Split('-');
            
            if (parts.Length != 2)
            {
                return NotFound();
            }

            if (parts[0] is not { Length: > 0 } root || !Note.TryParse(root, out var rootNote))
            {
                return NotFound();
            }

            if (parts[1] is not { Length: > 0 } arpeggioName || !Chords.TryGet(arpeggioName, out var arpeggio))
            {
                return NotFound();
            }

            var title = $"{rootNote.Display} {arpeggio.Name}";
            var fretboard = Fretboard.Create(frets ?? 12, openNoteArray);
            fretboard.SetBadges(arpeggio, rootNote);
            viewModel.Fretboards.Add(new FretboardViewModel(fretboard, title));
        }

        return View(viewModel);
    }

    [HttpGet("{openNotes}/chord/{chords}")]
    public IActionResult Chord(string openNotes, string chords, [FromQuery] int? frets, [FromQuery] string? tab)
    {
        var openNoteArray = ParseOpenNoteArray(openNotes);
        var viewModel = new FretboardPageViewModel
        {
            OpenNotes = openNotes,
            Frets = frets,
            Tab = tab ?? "Chords",
            Chords = chords
        };

        var chordArray = chords.Split('+');

        foreach (var chordStr in chordArray)
        {
            var parts = chordStr.Split('-');
            
            if (parts.Length != 2)
            {
                return NotFound();
            }

            if (parts[0] is not { Length: > 0 } root || !Note.TryParse(root, out var rootNote))
            {
                return NotFound();
            }

            if (parts[1] is not { Length: > 0 } chordName || !Chords.TryGet(chordName, out var chord))
            {
                return NotFound();
            }

            var title = $"{rootNote.Display} {chord.Name}";
            var fretboard = Fretboard.Create(frets ?? 12, openNoteArray);
            fretboard.SetBadges(chord, rootNote);
            viewModel.Fretboards.Add(new FretboardViewModel(fretboard, title));
        }

        return View(viewModel);
    }

    [HttpGet("{openNotes}/scale/{scales}")]
    public IActionResult Scale(string openNotes, string scales, [FromQuery] int? frets, [FromQuery] string? tab)
    {
        var openNoteArray = ParseOpenNoteArray(openNotes);
        var viewModel = new FretboardPageViewModel
        {
            OpenNotes = openNotes,
            Scales = scales,
            Frets = frets,
            Tab = tab ?? "Scales",
            PrintLink = Url.Action("Scale", "Print", new { tuning = openNotes, scales, frets })
        };

        var scalesArray = scales.Split('+');
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

            var fretboard = Fretboard.Create(frets ?? 12, openNoteArray);
            fretboard.SetBadges(scale, rootNote.Sign);

            var title = $"{rootNote.Display} {scaleSet.Name}";

            viewModel.Fretboards.Add(new FretboardViewModel(fretboard, title));
        }

        return View(viewModel);
    }

    private static Note[] ParseOpenNoteArray(string openNotes)
    {
        var list = new List<Note>();
        var notes = openNotes.AsSpan();
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

        var openNoteArray = list.ToArray();
        return openNoteArray;
    }
}