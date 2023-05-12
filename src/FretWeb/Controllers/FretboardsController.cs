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
    public IActionResult Get(string openNotes, [FromQuery] int? frets,
        [FromQuery(Name = "scale")] string? scaleName, [FromQuery] string? root,
        [FromQuery(Name = "chord")] string? chordId, [FromQuery] string? tab)
    {
        var openNoteArray = ParseOpenNoteArray(openNotes);
        
        var fretboard = Fretboard.Create(frets ?? 12, openNoteArray);

        var viewModel = new FretboardViewModel(fretboard)
        {
            OpenNotes = openNotes,
            Frets = frets,
            Scale = scaleName,
            Chord = chordId,
            Root = root,
            Tab = tab ?? "Scales"
        };

        if (root is not { Length: > 0 } || !Note.TryParse(root, out var rootNote))
        {
            return View(viewModel);
        }
        
        if (scaleName is { Length: > 0 })
        {
            var scaleSet = Scales.FindByName(scaleName);
            if (scaleSet is not null && scaleSet.TryGet(rootNote, out var scale))
            {
                viewModel.Title = $"{rootNote.Display} {scaleSet.Name}";
                
                fretboard.SetBadges(scale, rootNote.Sign);
            }
        }
        else if (chordId is { Length: > 0 })
        {
            if (Chords.TryGet(chordId, out var chord))
            {
                viewModel.Title = $"{rootNote.Display} {chord.Name}";

                fretboard.SetBadges(chord, rootNote);
            }
        }
        else
        {
            viewModel.Title = rootNote.Display;
            fretboard.SetBadges(rootNote);
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