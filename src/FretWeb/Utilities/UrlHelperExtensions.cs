﻿using FretWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace FretWeb.Utilities;

public static class UrlHelperExtensions
{
    public static string? Scale(this IUrlHelper urlHelper, string name) => urlHelper.Action("Get", "Scales", new { name });
    public static string? Fretboard(this IUrlHelper urlHelper, string tuning, string? scale = null, string? root = null,
        string? chord = null, int? frets = null, string? tab = null)
    {
        var routeValues = new RouteValueDictionary { { "tuning", tuning } };
        if (scale is { Length: > 0 })
        {
            routeValues.Add("scale", scale);
        }
        if (root is { Length: > 0 })
        {
            routeValues.Add("root", root);
        }
        if (chord is { Length: > 0 })
        {
            routeValues.Add("chord", chord);
        }
        if (tab is { Length: > 0 })
        {
            routeValues.Add("tab", tab);
        }
        if (frets.HasValue)
        {
            routeValues.Add("frets", frets.Value.ToString());
        }

        return urlHelper.Action("Get", "Fretboards", routeValues);
    }
    
    public static string? FretboardScale(this IUrlHelper urlHelper, FretboardPageViewModel fretboardPageViewModel, string scale, string root,
        int? frets = null, string? tab = null)
    {
        var routeValues = new RouteValueDictionary
        {
            { "tuning", fretboardPageViewModel.Tuning },
            { "scales", $"{root}-{scale}" },
        };
        
        routeValues.AddIfNotNull("tab", tab, fretboardPageViewModel.Tab);
        routeValues.AddIfNotNull("frets", frets, fretboardPageViewModel.Frets);

        return urlHelper.Action("Scale", "Fretboards", routeValues);
    }

    public static string? AddFretboardScale(this IUrlHelper urlHelper, FretboardPageViewModel fretboardPageViewModel, string scale, string root,
        int? frets = null, string? tab = null)
    {
        var routeValues = new RouteValueDictionary
        {
            { "tuning", fretboardPageViewModel.Tuning },
            { "scales", $"{fretboardPageViewModel.Scales}+{root}-{scale}" },
        };
        
        routeValues.AddIfNotNull("tab", tab, fretboardPageViewModel.Tab);
        routeValues.AddIfNotNull("frets", frets, fretboardPageViewModel.Frets);

        return urlHelper.Action("Scale", "Fretboards", routeValues);
    }

    public static string? FretboardMode(this IUrlHelper urlHelper, FretboardPageViewModel fretboardPageViewModel, string mode, string root,
        int? frets = null, string? tab = null)
    {
        var routeValues = new RouteValueDictionary
        {
            { "tuning", fretboardPageViewModel.Tuning },
            { "modes", $"{root}-{mode}" },
        };
        
        routeValues.AddIfNotNull("tab", tab, fretboardPageViewModel.Tab);
        routeValues.AddIfNotNull("frets", frets, fretboardPageViewModel.Frets);

        return urlHelper.Action("Mode", "Fretboards", routeValues);
    }

    public static string? AddFretboardMode(this IUrlHelper urlHelper, FretboardPageViewModel fretboardPageViewModel, string mode, string root,
        int? frets = null, string? tab = null)
    {
        var routeValues = new RouteValueDictionary
        {
            { "tuning", fretboardPageViewModel.Tuning },
            { "modes", $"{fretboardPageViewModel.Modes}+{root}-{mode}" },
        };
        
        routeValues.AddIfNotNull("tab", tab, fretboardPageViewModel.Tab);
        routeValues.AddIfNotNull("frets", frets, fretboardPageViewModel.Frets);

        return urlHelper.Action("Mode", "Fretboards", routeValues);
    }

    public static string? FretboardChord(this IUrlHelper urlHelper, FretboardPageViewModel fretboardPageViewModel, string chord, string root,
        int? frets = null, string? tab = null)
    {
        var routeValues = new RouteValueDictionary
        {
            { "tuning", fretboardPageViewModel.Tuning },
            { "chords", $"{root}-{chord}" },
        };
        
        routeValues.AddIfNotNull("tab", tab, fretboardPageViewModel.Tab);
        routeValues.AddIfNotNull("frets", frets, fretboardPageViewModel.Frets);

        return urlHelper.Action("Chord", "Fretboards", routeValues);
    }

    public static string? AddFretboardChord(this IUrlHelper urlHelper, FretboardPageViewModel fretboardPageViewModel, string chord, string root,
        int? frets = null, string? tab = null)
    {
        var routeValues = new RouteValueDictionary
        {
            { "tuning", fretboardPageViewModel.Tuning },
            { "chords", $"{fretboardPageViewModel.Chords}+{root}-{chord}" },
        };
        
        routeValues.AddIfNotNull("tab", tab, fretboardPageViewModel.Tab);
        routeValues.AddIfNotNull("frets", frets, fretboardPageViewModel.Frets);

        return urlHelper.Action("Chord", "Fretboards", routeValues);
    }

    public static string? FretboardArpeggio(this IUrlHelper urlHelper, FretboardPageViewModel fretboardPageViewModel, string arpeggio, string root,
        int? frets = null, string? tab = null)
    {
        var routeValues = new RouteValueDictionary
        {
            { "tuning", fretboardPageViewModel.Tuning },
            { "arpeggios", $"{root}-{arpeggio}" },
        };
        
        routeValues.AddIfNotNull("tab", tab, fretboardPageViewModel.Tab);
        routeValues.AddIfNotNull("frets", frets, fretboardPageViewModel.Frets);

        return urlHelper.Action("Arpeggio", "Fretboards", routeValues);
    }

    public static string? AddFretboardArpeggio(this IUrlHelper urlHelper, FretboardPageViewModel fretboardPageViewModel, string arpeggio, string root,
        int? frets = null, string? tab = null)
    {
        var routeValues = new RouteValueDictionary
        {
            { "tuning", fretboardPageViewModel.Tuning },
            { "arpeggios", $"{fretboardPageViewModel.Arpeggios}+{root}-{arpeggio}" },
        };
        
        routeValues.AddIfNotNull("tab", tab, fretboardPageViewModel.Tab);
        routeValues.AddIfNotNull("frets", frets, fretboardPageViewModel.Frets);

        return urlHelper.Action("Arpeggio", "Fretboards", routeValues);
    }

    public static string? FretboardNote(this IUrlHelper urlHelper, FretboardPageViewModel fretboardPageViewModel, string root,
        int? frets = null, string? tab = null)
    {
        var routeValues = new RouteValueDictionary
        {
            { "tuning", fretboardPageViewModel.Tuning },
            { "root", root }
        };
        
        routeValues.AddIfNotNull("tab", tab, fretboardPageViewModel.Tab);
        routeValues.AddIfNotNull("frets", frets, fretboardPageViewModel.Frets);

        return urlHelper.Action("Get", "Fretboards", routeValues);
    }
    
    public static string? Fretboard(this IUrlHelper urlHelper, FretboardPageViewModel fretboardPageViewModel, string? scale = null, string? root = null,
        string? chord = null, int? frets = null, string? tab = null)
    {
        var routeValues = new RouteValueDictionary { { "tuning", fretboardPageViewModel.Tuning } };
        routeValues.AddIfNotNull("tab", tab, fretboardPageViewModel.Tab);
        routeValues.AddIfNotNull("frets", frets, fretboardPageViewModel.Frets);
        
        if (fretboardPageViewModel.Scales is { Length: > 0 })
        {
            routeValues.Add("scales", fretboardPageViewModel.Scales);
            return urlHelper.Action("Scale", "Fretboards", routeValues);
        }

        if (fretboardPageViewModel.Arpeggios is { Length: > 0 })
        {
            routeValues.Add("arpeggios", fretboardPageViewModel.Arpeggios);
            return urlHelper.Action("Arpeggio", "Fretboards", routeValues);
        }

        if (fretboardPageViewModel.Chords is { Length: > 0 })
        {
            routeValues.Add("chords", fretboardPageViewModel.Chords);
            return urlHelper.Action("Chord", "Fretboards", routeValues);
        }

        if (chord is null)
        {
            routeValues.AddIfNotNull("scale", scale, fretboardPageViewModel.Scale);
        }
        else if (scale is null)
        {
            routeValues.AddIfNotNull("chord", chord, fretboardPageViewModel.Chord);
        }

        routeValues.AddIfNotNull("root", root, fretboardPageViewModel.Root);

        return urlHelper.Action("Get", "Fretboards", routeValues);
    }

    public static string? RemoveFretboard(this IUrlHelper urlHelper, string fretboardId)
    {
        return UrlHelpers.RemoveFretboard(urlHelper.ActionContext.HttpContext.Request.Path.ToString(),
            urlHelper.ActionContext.HttpContext.Request.QueryString.ToString(),
            fretboardId);
    }
    
    private static void AddIfNotNull(this RouteValueDictionary routeValues, string key, string? first, string? second)
    {
        if (first is { Length: > 0 })
        {
            routeValues.Add(key, first);
        }
        else if (second is { Length: > 0 })
        {
            routeValues.Add(key, second);
        }
    }

    private static void AddIfNotNull(this RouteValueDictionary routeValues, string key, int? first, int? second)
    {
        if (first.HasValue)
        {
            routeValues.Add(key, first.Value);
        }
        else if (second.HasValue)
        {
            routeValues.Add(key, second.Value);
        }
    }
}