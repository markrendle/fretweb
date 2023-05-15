namespace FretWeb.Utilities;

public static class RequestExtensions
{
    public static Theme GetTheme(this HttpRequest request)
    {
        if (request.Cookies.TryGetValue("fretweb.theme", out var themeStr))
        {
            if (Enum.TryParse(themeStr, true, out Theme theme))
            {
                return theme;
            }
        }

        return Theme.Light;
    }

    public static string SetThemeUrl(this HttpRequest request, Theme theme)
    {
        var path = request.Path.ToString();
        var query = request.QueryString.ToString();
        if (query is { Length: > 0 })
        {
            query += $"&theme={theme.ToString().ToLower()}";
        }
        else
        {
            query += $"?theme={theme.ToString().ToLower()}";
        }

        return path + query;
    }
}