using FretWeb.Fretboards;
using FretWeb.Models;
using FretWeb.Music;
using FretWeb.Music.NoteTypes;
using Microsoft.AspNetCore.Mvc;

namespace FretWeb.Controllers;

[Route("fretboards")]
public class FretboardsController : Controller
{
    [HttpGet("{tuning}")]
    public IActionResult Get(string tuning, [FromQuery] int? frets, [FromQuery] string? tab)
    {
        var tuningArray = ParseTuningArray(tuning);

        var fretboard = Fretboard.Create(frets ?? 12, tuningArray);

        var viewModel = new FretboardPageViewModel
        {
            Tuning = tuning,
            Frets = frets,
            Tab = tab ?? "Scales"
        };
        
        viewModel.Fretboards.Add(new FretboardViewModel(fretboard, Array.Empty<Note>(), "default", tuning));

        return View(viewModel);
    }

    [HttpGet("{tuning}/note/{noteId}")]
    public IActionResult Notes(string tuning, string noteId, [FromQuery] int? frets, [FromQuery] string? tab, [FromQuery] bool? print = false)
    {
        if (!Note.TryParse(noteId, out var note)) return NotFound();
        
        var tuningArray = ParseTuningArray(tuning);

        var fretboard = Fretboard.Create(frets ?? 12, tuningArray);
        fretboard.SetBadges(note);

        var viewModel = new FretboardPageViewModel
        {
            Tuning = tuning,
            Frets = frets,
            Tab = tab ?? "Notes",
            Print = print.GetValueOrDefault(),
            PrintLink = print.GetValueOrDefault() ? null : Url.Action("Arpeggio", new { tuning, noteId, frets, tab, print = true })
        };
        
        viewModel.Fretboards.Add(new FretboardViewModel(fretboard, new[] { note }, noteId.ToLowerInvariant(), note.Display));

        return View(viewModel);
    }

    [HttpGet("{tuning}/arpeggio/{arpeggios}")]
    public IActionResult Arpeggio(string tuning, string arpeggios, [FromQuery] int? frets, [FromQuery] string? tab, [FromQuery] bool? print = false)
    {
        var tuningArray = ParseTuningArray(tuning);
        var viewModel = new FretboardPageViewModel
        {
            Tuning = tuning,
            Frets = frets,
            Tab = tab ?? "Arpeggios",
            Arpeggios = arpeggios,
            Print = print.GetValueOrDefault(),
            PrintLink = print.GetValueOrDefault() ? null : Url.Action("Arpeggio", new { tuning, arpeggios, frets, tab, print = true })
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

            if (parts[1] is not { Length: > 0 } arpeggioName || !Arpeggios.TryGet(arpeggioName, out var arpeggio))
            {
                return NotFound();
            }

            var title = $"{rootNote.Display} {arpeggio.Name}";
            var fretboard = Fretboard.Create(frets ?? 12, tuningArray);
            fretboard.SetBadges(arpeggio, rootNote);
            viewModel.Fretboards.Add(new FretboardViewModel(fretboard, arpeggio.GetNotes(rootNote), arpeggioStr.ToLowerInvariant(), title));
        }

        return View(viewModel);
    }

    [HttpGet("{tuning}/chord/{chords}")]
    public IActionResult Chord(string tuning, string chords, [FromQuery] int? frets, [FromQuery] string? tab, [FromQuery] bool? print = false)
    {
        var tuningArray = ParseTuningArray(tuning);
        var viewModel = new FretboardPageViewModel
        {
            Tuning = tuning,
            Frets = frets,
            Tab = tab ?? "Chords",
            Chords = chords,
            Print = print.GetValueOrDefault(),
            PrintLink = print.GetValueOrDefault() ? null : Url.Action("Chord", new { tuning, chords, frets, tab, print = true })
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

            if (parts[1] is not { Length: > 0 } chordName || !Arpeggios.TryGet(chordName, out var chord))
            {
                return NotFound();
            }

            var title = $"{rootNote.Display} {chord.Name}";
            var fretboard = Fretboard.Create(frets ?? 12, tuningArray);
            fretboard.SetBadges(chord, rootNote);
            viewModel.Fretboards.Add(new FretboardViewModel(fretboard, chord.GetNotes(rootNote), chordStr.ToLowerInvariant(), title));
        }

        return View(viewModel);
    }

    [HttpGet("{tuning}/scale/{scales}")]
    public IActionResult Scale(string tuning, string scales, [FromQuery] int? frets, [FromQuery] string? tab, [FromQuery] bool? print = false)
    {
        var tuningArray = ParseTuningArray(tuning);
        var viewModel = new FretboardPageViewModel
        {
            Tuning = tuning,
            Scales = scales,
            Frets = frets,
            Tab = tab ?? "Scales",
            Print = print.GetValueOrDefault(),
            PrintLink = print.GetValueOrDefault() ? null : Url.Action("Scale", new { tuning, scales, frets, tab, print = true })
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

            if (parts[1] is not { Length: > 0 } scaleName || !(Scales.FindById(scaleName) is { } scaleSet) || !scaleSet.TryGet(rootNote, out var scale))
            {
                return NotFound();
            }

            var fretboard = Fretboard.Create(frets ?? 12, tuningArray);
            fretboard.SetBadges(scale, rootNote.Sign);

            var title = $"{rootNote.Display} {scaleSet.Id}";

            viewModel.Fretboards.Add(new FretboardViewModel(fretboard, scale.ToArray(), scaleStr.ToLowerInvariant(), title));
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

            list.Add(Music.Notes.Get(n, sign));
        }

        var openNoteArray = list.ToArray();
        return openNoteArray;
    }
}