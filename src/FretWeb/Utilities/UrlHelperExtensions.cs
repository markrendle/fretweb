using FretWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace FretWeb.Utilities;

public static class UrlHelperExtensions
{
    public static string? Scale(this IUrlHelper urlHelper, string name) => urlHelper.Action("Get", "Scales", new { name });
    public static string? Fretboard(this IUrlHelper urlHelper, string openNotes, string? scale = null, string? root = null,
        string? chord = null, int? frets = null, string? tab = null)
    {
        var routeValues = new RouteValueDictionary { { "openNotes", openNotes } };
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
            { "openNotes", fretboardPageViewModel.OpenNotes },
            { "scales", $"{root}-{scale}" },
        };
        
        routeValues.AddIfNotNull("tab", tab, fretboardPageViewModel.Tab);
        routeValues.AddIfNotNull("frets", frets, fretboardPageViewModel.Frets);

        return urlHelper.Action("Scale", "Fretboards", routeValues);
    }

    public static string? FretboardChord(this IUrlHelper urlHelper, FretboardPageViewModel fretboardPageViewModel, string chord, string root,
        int? frets = null, string? tab = null)
    {
        var routeValues = new RouteValueDictionary
        {
            { "openNotes", fretboardPageViewModel.OpenNotes },
            { "chord", chord },
            { "root", root }
        };
        
        routeValues.AddIfNotNull("tab", tab, fretboardPageViewModel.Tab);
        routeValues.AddIfNotNull("frets", frets, fretboardPageViewModel.Frets);

        return urlHelper.Action("Get", "Fretboards", routeValues);
    }

    public static string? FretboardNote(this IUrlHelper urlHelper, FretboardPageViewModel fretboardPageViewModel, string root,
        int? frets = null, string? tab = null)
    {
        var routeValues = new RouteValueDictionary
        {
            { "openNotes", fretboardPageViewModel.OpenNotes },
            { "root", root }
        };
        
        routeValues.AddIfNotNull("tab", tab, fretboardPageViewModel.Tab);
        routeValues.AddIfNotNull("frets", frets, fretboardPageViewModel.Frets);

        return urlHelper.Action("Get", "Fretboards", routeValues);
    }
    
    public static string? Fretboard(this IUrlHelper urlHelper, FretboardPageViewModel fretboardPageViewModel, string? scale = null, string? root = null,
        string? chord = null, int? frets = null, string? tab = null)
    {
        var routeValues = new RouteValueDictionary { { "openNotes", fretboardPageViewModel.OpenNotes } };
        routeValues.AddIfNotNull("tab", tab, fretboardPageViewModel.Tab);
        routeValues.AddIfNotNull("frets", frets, fretboardPageViewModel.Frets);
        
        if (fretboardPageViewModel.Scales is { Length: > 0 })
        {
            routeValues.Add("scales", fretboardPageViewModel.Scales);
            return urlHelper.Action("Scale", "Fretboards", routeValues);
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