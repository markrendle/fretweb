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
        if (model.Tab == "add") return View(AddViewModel(model));
        return View(ScalesViewModel(model));
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

    private NavigationViewModel ArpeggiosViewModel(FretboardPageViewModel model)
    {
        var viewModel = new NavigationViewModel();

        foreach (var arpeggio in Arpeggios.All())
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
                    Href = Url.FretboardArpeggio(model, arpeggio.Id, note.Id, tab: "add")!,
                    Title = $"{note.Display} {arpeggio.Name}",
                    Text = note.Text,
                    Display = note.Display,
                };
                row.Buttons.Add(button);
            }
        }

        return viewModel;
    }

    private NavigationViewModel ChordsViewModel(FretboardPageViewModel model)
    {
        var viewModel = new NavigationViewModel();

        foreach (var arpeggio in Arpeggios.All().Where(a => a.Count < 6))
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
                    Href = Url.FretboardChord(model, arpeggio.Id, note.Id, tab: "add")!,
                    Title = $"{note.Display} {arpeggio.Name}",
                    Text = note.Text,
                    Display = note.Display,
                };
                row.Buttons.Add(button);
            }
        }

        return viewModel;
    }

    private NavigationViewModel ScalesViewModel(FretboardPageViewModel model)
    {
        var viewModel = new NavigationViewModel();

        foreach (var scaleSet in Scales.EnumerateScales())
        {
            var row = new NavigationViewModel.Row
            {
                Name = scaleSet.Name,
                Id = scaleSet.Id,
            };
            viewModel.Rows.Add(row);
            foreach (var scale in scaleSet.Enumerate())
            {
                var href = Url.FretboardScale(model, scale: scaleSet.Id, root: scale[0].Id, tab: "add");
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
    
    private NavigationViewModel ModesViewModel(FretboardPageViewModel model)
    {
        var viewModel = new NavigationViewModel();

        foreach (var scaleSet in Scales.EnumerateModes())
        {
            var row = new NavigationViewModel.Row
            {
                Name = scaleSet.Name,
                Id = scaleSet.Id,
            };
            viewModel.Rows.Add(row);
            foreach (var scale in scaleSet.Enumerate())
            {
                var href = Url.FretboardMode(model, mode: scaleSet.Id, root: scale[0].Id, tab: "add");
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
    
    private NavigationViewModel? AddViewModel(FretboardPageViewModel model)
    {
        if (model.Arpeggios is { Length: > 0 })
        {
            return AddArpeggiosViewModel(model);
        }

        if (model.Chords is { Length: > 0 })
        {
            return AddChordsViewModel(model);
        }

        if (model.Scales is { Length: > 0 })
        {
            return AddScalesViewModel(model);
        }

        if (model.Modes is { Length: > 0 })
        {
            return AddModesViewModel(model);
        }

        return null;
    }

    public static bool CanAdd(FretboardPageViewModel model) =>
        model.Arpeggios is { Length: > 0 } || model.Chords is { Length: > 0 } || model.Scales is { Length: > 0 };

    private NavigationViewModel AddArpeggiosViewModel(FretboardPageViewModel model)
    {
        var viewModel = new NavigationViewModel();
        
        foreach (var arpeggio in Arpeggios.All())
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
                    Href = Url.AddFretboardArpeggio(model, arpeggio.Id, note.Id)!,
                    Title = $"{note.Display} {arpeggio.Name}",
                    Text = note.Text,
                    Display = note.Display,
                };
                row.Buttons.Add(button);
            }
        }

        return viewModel;
    }
    
    private NavigationViewModel AddScalesViewModel(FretboardPageViewModel model)
    {
        var viewModel = new NavigationViewModel();

        foreach (var scaleSet in Scales.EnumerateScales())
        {
            var row = new NavigationViewModel.Row
            {
                Name = scaleSet.Name,
                Id = scaleSet.Id,
            };
            viewModel.Rows.Add(row);
            foreach (var scale in scaleSet.Enumerate())
            {
                var href = Url.AddFretboardScale(model, scale: scaleSet.Id, root: scale[0].Id, tab: "add");
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

    private NavigationViewModel AddModesViewModel(FretboardPageViewModel model)
    {
        var viewModel = new NavigationViewModel();

        foreach (var scaleSet in Scales.EnumerateModes())
        {
            var row = new NavigationViewModel.Row
            {
                Name = scaleSet.Name,
                Id = scaleSet.Id,
            };
            viewModel.Rows.Add(row);
            foreach (var scale in scaleSet.Enumerate())
            {
                var href = Url.AddFretboardMode(model, mode: scaleSet.Id, root: scale[0].Id, tab: "add");
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
    
    private NavigationViewModel AddChordsViewModel(FretboardPageViewModel model)
    {
        var viewModel = new NavigationViewModel();

        foreach (var arpeggio in Arpeggios.All().Where(a => a.Count < 6))
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
                    Href = Url.AddFretboardChord(model, arpeggio.Id, note.Id, tab: "add")!,
                    Title = $"{note.Display} {arpeggio.Name}",
                    Text = note.Text,
                    Display = note.Display,
                };
                row.Buttons.Add(button);
            }
        }

        return viewModel;
    }

}