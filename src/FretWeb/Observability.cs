using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace FretWeb;

public static class Observability
{
    public static readonly ActivitySource ActivitySource = new("FretWeb", "1.0");
    public static readonly Meter Meter = new("FretWeb", "1.0");
}