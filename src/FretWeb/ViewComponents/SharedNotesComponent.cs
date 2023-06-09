using FretWeb.Fretboards;
using FretWeb.Models;
using FretWeb.Music;
using FretWeb.Music.NoteTypes;
using Microsoft.AspNetCore.Mvc;

namespace FretWeb.ViewComponents;

public class SharedNotesViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(FretboardViewModel previous, FretboardViewModel next)
    {
        var commonNotes = CommonNotes(previous.Notes, next.Notes);
        var model = new SharedNotesViewModel(commonNotes);
        return View(model);
    }

    private IEnumerable<Note> CommonNotes(Note[] previous, Note[] next)
    {
        return previous.Where(note => next.Any(n => n.IsEquivalentTo(note)));
    }

    private static Note PreferFlat(Note note) => note.Sign switch
    {
        Sign.Natural => note,
        Sign.Flat => note,
        Sign.Sharp => note.AsFlat(),
        Sign.FlatFlat => note.Alt,
        Sign.SharpSharp => note.Alt,
        _ => throw new ArgumentOutOfRangeException()
    };
}