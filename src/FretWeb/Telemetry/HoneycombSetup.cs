using System.Reflection;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace FretWeb.Telemetry;

public static class HoneycombSetup
{
    public static void AddHoneycombOpenTelemetry(this WebApplicationBuilder builder)
    {
        var honeycombOptions = builder.Configuration.GetHoneycombOptions();
        honeycombOptions.MetricsDataset ??= honeycombOptions.Dataset ??= "fretweb";

        if (honeycombOptions.ApiKey is not { Length: > 0 }) return;
        
        var resourceBuilder = ResourceBuilder.CreateEmpty()
            .AddService("fretweb", serviceVersion: Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "1.0.0");
        builder.Logging.AddOpenTelemetry(options =>
        {
            options.SetResourceBuilder(resourceBuilder);
            options.AddOtlpExporter(o =>
            {
                o.Endpoint = new Uri("https://api.honeycomb.io");
                o.Headers = $"x-honeycomb-team={honeycombOptions.ApiKey}";
            });
        });

        builder.Services.AddOpenTelemetry()
            .WithTracing(otel =>
            {
                otel.AddHoneycomb(honeycombOptions)
                    .AddAspNetCoreInstrumentationWithBaggage()
                    .SetSampler(new TraceIdRatioBasedSampler(0.1));
            })
            .WithMetrics(otel =>
            {
                otel.AddHoneycomb(honeycombOptions)
                    .AddAspNetCoreInstrumentation();
            });
    }
}