using FretWeb.Services;
using FretWeb.Telemetry;
using FretWeb.Utilities;
using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

builder.AddHoneycombOpenTelemetry();

// Add services to the container.
builder.Services.AddServerSideBlazor();
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddHealthChecks();

if (EnvironmentVariables.HaveValues("CLOUDFLARE_API_TOKEN", "CLOUDFLARE_API_ZONE"))
{
    builder.Services.AddHostedService<CloudflarePurgeService>();
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

const string cacheHeader = "public,max-age=86400";

app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        ctx.Context.Response.Headers[HeaderNames.CacheControl] = cacheHeader;
    }
});

app.UseRouting();

if (Environment.GetEnvironmentVariable("GOOGLE_ADS_TXT") is { Length: > 0 } googleAdsTxt)
{
    app.MapGet("/ads.txt", context =>
    {
        context.Response.ContentType = "text/text";
        context.Response.ContentLength = googleAdsTxt.Length;
        return context.Response.WriteAsync(googleAdsTxt);
    });
}

app.MapHealthChecks("/health");

Func<object, Task> setResponseHeaders = state =>
{
    var headers = ((HttpContext)state).Response.Headers;
    headers.Add("X-Clacks-Overhead", "GNU Terry Pratchett");
    headers.CacheControl = cacheHeader;
    headers.Vary = "Accept-Encoding";
    return Task.CompletedTask;
};

app.Use(async (context, next) =>
{
    context.Response.OnStarting(setResponseHeaders, context);
    await next();
});

app.UseAuthorization();

// app.Use(ContentLengthMiddleware.SetContentLength);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
