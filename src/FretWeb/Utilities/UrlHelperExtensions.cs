using FretWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace FretWeb.Utilities;

public static class UrlHelperExtensions
{
    public static string? Scale(this IUrlHelper urlHelper, string name) => urlHelper.Action("Get", "Scales", new { name });
    public static string? Fretboard(this IUrlHelper urlHelper, string openNotes, string? scale = null, string? root = null, int? frets = null,
        string? tab = null)
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
    
    public static string? Fretboard(this IUrlHelper urlHelper, FretboardViewModel fretboardViewModel, string? scale = null, string? root = null, int? frets = null,
        string? tab = null)
    {
        var routeValues = new RouteValueDictionary { { "openNotes", fretboardViewModel.OpenNotes } };
        
        routeValues.AddIfNotNull("scale", scale, fretboardViewModel.Scale);
        routeValues.AddIfNotNull("root", root, fretboardViewModel.Root);
        routeValues.AddIfNotNull("tab", tab, fretboardViewModel.Tab);
        routeValues.AddIfNotNull("frets", frets, fretboardViewModel.Frets);

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