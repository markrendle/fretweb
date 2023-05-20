using FretWeb.Services;
using FretWeb.Telemetry;
using FretWeb.Utilities;
using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

builder.AddHoneycombOpenTelemetry();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddResponseCaching();

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

const int duration = 60 * 60;
var cacheHeader = $"public,max-age={duration}";
app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx => { ctx.Context.Response.Headers[HeaderNames.CacheControl] = cacheHeader; }
});

app.UseRouting();

app.Use(async (context, next) =>
{
    context.Response.Headers.CacheControl = cacheHeader;
    context.Response.Headers.Vary = "Accept-Encoding";
    await next();
});

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
