using FretWeb.Models;
using FretWeb.Music;
using FretWeb.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace FretWeb.ViewComponents;

public class NavigationViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(FretboardPageViewModel model)
    {
        if (model.Tab == "modes") return View(ModesViewModel(model));
        if (model.Tab == "arpeggios") return View(ArpeggiosViewModel(model));
        if (model.Tab == "chords") return View(ChordsViewModel(model));
        if (model.Tab == "notes") return View(NotesViewModel(model));
        return View(ScalesViewModel(model));
    }

    private NavigationViewModel ScalesViewModel(FretboardPageViewModel model)
    {
        return ScaleSetsViewModel(model, Scales.EnumerateScales());
    }

    private NavigationViewModel ModesViewModel(FretboardPageViewModel model)
    {
        return ScaleSetsViewModel(model, Scales.EnumerateModes());
    }

    private NavigationViewModel ArpeggiosViewModel(FretboardPageViewModel model)
    {
        return ArpeggiosViewModel(model, Arpeggios.All());
    }

    private NavigationViewModel ChordsViewModel(FretboardPageViewModel model)
    {
        return ArpeggiosViewModel(model, Arpeggios.All().Where(a => a.Count < 6));
    }

    private NavigationViewModel NotesViewModel(FretboardPageViewModel model)
    {
        var viewModel = new NavigationViewModel();
        var row = new NavigationViewModel.Row
        {
            Id = "notes",
            Name = "Notes"
        };
        viewModel.Rows.Add(row);
        foreach (var note in Notes.ChromaticWithFlats())
        {
            var button = new NavigationViewModel.Item
            {
                Href = Url.FretboardNote(model, root: note.Text)!,
                Display = note.Display,
                Text = note.Text,
                Title = note.Display,
            };
            row.Buttons.Add(button);
        }

        return viewModel;
    }

    private NavigationViewModel ArpeggiosViewModel(FretboardPageViewModel model, IEnumerable<Arpeggio> arpeggios)
    {
        var viewModel = new NavigationViewModel();

        foreach (var arpeggio in arpeggios)
        {
            var row = new NavigationViewModel.Row
            {
                Name = arpeggio.Name,
                Id = arpeggio.Id
            };
            viewModel.Rows.Add(row);
            foreach (var note in Notes.ChromaticWithFlats())
            {
                var button = new NavigationViewModel.Item
                {
                    Href = Url.FretboardArpeggio(model, arpeggio.Id, note.Id)!,
                    Title = $"{note.Display} {arpeggio.Name}",
                    Text = note.Text,
                    Display = note.Display,
                };
                row.Buttons.Add(button);
            }
        }

        return viewModel;
    }

    private NavigationViewModel ScaleSetsViewModel(FretboardPageViewModel model, IEnumerable<ScaleSet> scaleSets)
    {
        var viewModel = new NavigationViewModel();

        foreach (var scaleSet in scaleSets)
        {
            var row = new NavigationViewModel.Row
            {
                Name = scaleSet.Name,
                Id = scaleSet.Id,
            };
            viewModel.Rows.Add(row);
            foreach (var scale in scaleSet.Enumerate())
            {
                var href = Url.FretboardScale(model, scale: scaleSet.Id, root: scale[0].Id);
                if (href is not {Length: > 0}) continue;
                var item = new NavigationViewModel.Item
                {
                    Title = $"{scale[0].Display} {scaleSet.Id}",
                    Display = scale[0].Display,
                    Text = scale[0].Text,
                    Href = href,
                };
                row.Buttons.Add(item);
            }
        }

        return viewModel;
    }
}