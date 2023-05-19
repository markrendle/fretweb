using FretWeb.Telemetry;
using FretWeb.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

builder.AddHoneycombOpenTelemetry();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddResponseCaching();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

const int duration = 60 * 60;
var cacheHeader = $"public,max-age={duration}";
app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx => { ctx.Context.Response.Headers[HeaderNames.CacheControl] = cacheHeader; }
});

app.Use(async (context, next) =>
{
    if (context.Request.Query.TryGetValue("theme", out var values))
    {
        if (Enum.TryParse(values.FirstOrDefault(), true, out Theme theme))
        {
            var themeStr = theme.ToString().ToLower();
            context.Response.Cookies.Append("fretweb.theme", themeStr);
            context.Response.Headers.TryAdd("X-Theme", themeStr);
        }

        var path = context.Request.Path;
        if (context.Request.Query.Count == 1)
        {
            context.Response.Redirect(path);
        }
        else
        {
            var query = context.Request.QueryString.ToString().Replace($"&theme={theme.ToString().ToLower()}", "");
            context.Response.Redirect($"{path}{query}");
        }

        return;
    }
    await next();
});

app.UseRouting();

app.Use(async (context, next) =>
{
    // context.Response.Headers.CacheControl = cacheHeader;
    // context.Response.Headers.Vary = "X-Theme, Accept-Encoding";
    if (!context.Request.Cookies.TryGetValue("fretweb.theme", out var theme))
    {
        theme = "light";
    }
    context.Response.Headers.TryAdd("X-Theme", theme);
    await next();
});

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();