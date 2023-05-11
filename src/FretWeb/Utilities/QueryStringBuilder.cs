using System.Web;

namespace FretWeb.Utilities;

public class QueryStringBuilder
{
    private readonly List<(string, string)> _values = new();

    public void Clear() => _values.Clear();

    public void Add(string key, string? value)
    {
        if (value is { Length: > 0 })
        {
            _values.Add((key, HttpUtility.UrlEncode(value)));
        }
    }

    public void Add(string key, int? value)
    {
        if (value.HasValue)
        {
            _values.Add((key, value.Value.ToString()));
        }
    }

    public string Build()
    {
        if (_values.Count == 0) return string.Empty;
        if (_values.Count == 1) return $"?{_values[0].Item1}={_values[0].Item2}";
        return "?" + string.Join('&', _values.Select(t => $"{t.Item1}={t.Item2}"));
    }
}